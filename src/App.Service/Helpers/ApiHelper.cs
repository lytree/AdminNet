using App.Core.Configs;
using App.Core.Attributes;
using App.Service.Tools.Cache;
using App.Repository.Domain;
using Microsoft.Extensions.Options;
using App.Service.Services;
using System.Reflection;
using Framework.System;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml;
using App.Service.Consts;

namespace App.Core.Helpers;

/// <summary>
/// Api帮助类
/// </summary>
[InjectSingleton]
public class ApiHelper
{
    static int _CodeBaseNotSupportedException = 0;
    private readonly ICacheTool _cacheTool;
    private readonly IApiRepository _apiRepository;
    private readonly IOptions<AppConfig> _appConfig;

    public ApiHelper(ICacheTool cacheTool, IApiRepository apiRepository, IOptions<AppConfig> appConfig)
    {
        _cacheTool = cacheTool;
        _apiRepository = apiRepository;
        _appConfig = appConfig;
    }

    public async Task<List<ApiModel>> GetApiListAsync()
    {
        return await _cacheTool.GetOrSetAsync(CacheKeys.ApiList, async () =>
        {
            var apis = await _apiRepository.Select.ToListAsync(a => new { a.Id, a.ParentId, a.Label, a.Path, a.EnabledLog, a.EnabledParams, a.EnabledResult });

            var apiList = new List<ApiModel>();
            foreach (var api in apis)
            {
                var parentLabel = apis.FirstOrDefault(a => a.Id == api.ParentId)?.Label;

                apiList.Add(new ApiModel
                {
                    Label = parentLabel.NotNull() ? $"{parentLabel} / {api.Label}" : api.Label,
                    Path = api.Path?.ToLower().Trim('/'),
                    EnabledLog = api.EnabledLog,
                    EnabledParams = api.EnabledParams,
                    EnabledResult = api.EnabledResult,
                });
            }

            return apiList;
        });
    }

    public List<ApiGetEnumsOutput> GetEnumList()
    {
        var enums = new List<ApiGetEnumsOutput>();

        var appConfig = _appConfig.Value;
        var assemblyNames = appConfig.EnumListAssemblyNames;
        if (!(assemblyNames?.Length > 0))
        {
            return enums;
        }

        foreach (var assemblyName in assemblyNames)
        {
            var assembly = Assembly.Load(assemblyName);
            var enumTypes = assembly.GetTypes().Where(m => m.IsEnum);
            foreach (var enumType in enumTypes)
            {
                var summaryList = GetEnumSummaryList(enumType);

                var enumDescriptor = new ApiGetEnumsOutput
                {
                    Name = enumType.Name,
                    Desc = enumType.GetDescription() ?? (summaryList.TryGetValue("", out var comment) ? comment : ""),
                    Options = Enum.GetValues(enumType).Cast<Enum>().Select(x => new ApiGetEnumsOutput.Models.Options
                    {
                        Name = x.ToString(),
                        Desc = x.GetDescription() ?? (summaryList.TryGetValue(x.ToString(), out var comment) ? comment : ""),
                        Value = x.ToInt64()
                    }).ToList()
                };

                enums.Add(enumDescriptor);
            }
        }

        return enums;
    }
    

    /// <summary>
    /// 获得枚举类型说明列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetEnumSummaryList(Type type)
    {
        return LocalGetComment(type, 0);

        Dictionary<string, string> LocalGetComment(Type localType, int level)
        {
            if (localType.Assembly.IsDynamic) return null;
            //动态生成的程序集，访问不了 Assembly.Location/Assembly.CodeBase
            var regex = new Regex(@"\.(dll|exe)", RegexOptions.IgnoreCase);
            var xmlPath = regex.Replace(localType.Assembly.Location, ".xml");
            if (File.Exists(xmlPath) == false)
            {
                if (_CodeBaseNotSupportedException == 1) return null;
                try
                {
                    if (string.IsNullOrEmpty(localType.Assembly.Location)) return null;
                }
                catch (NotSupportedException) //NotSupportedException: CodeBase is not supported on assemblies loaded from a single-file bundle.
                {
                    Interlocked.Exchange(ref _CodeBaseNotSupportedException, 1);
                    return null;
                }

                xmlPath = regex.Replace(localType.Assembly.Location, ".xml");
                if (xmlPath.StartsWith("file:///") && Uri.TryCreate(xmlPath, UriKind.Absolute, out var tryuri))
                    xmlPath = tryuri.LocalPath;
                if (File.Exists(xmlPath) == false) return null;
            }

            var dic = new Dictionary<string, string>();
            StringReader sReader = null;
            try
            {
                sReader = new StringReader(File.ReadAllText(xmlPath));
            }
            catch
            {
                return dic;
            }
            using (var xmlReader = XmlReader.Create(sReader))
            {
                XPathDocument xpath = null;
                try
                {
                    xpath = new XPathDocument(xmlReader);
                }
                catch
                {
                    return null;
                }
                var xmlNav = xpath.CreateNavigator();

                var className = (localType.IsNested ? (localType.DeclaringType != null && localType.DeclaringType.DeclaringType != null &&
                    localType.DeclaringType.DeclaringType.FullName.NotNull() ? $"{localType.DeclaringType.DeclaringType.FullName}.{localType.DeclaringType.Name}.{localType.Name}" :
                    $"{localType.Namespace}.{localType.DeclaringType.Name}.{localType.Name}") :
                    $"{localType.Namespace}.{localType.Name}").Trim('.');

                var node = xmlNav.SelectSingleNode($"/doc/members/member[@name='T:{className}']/summary");
                if (node != null)
                {
                    var comment = node.InnerXml.Trim(' ', '\r', '\n', '\t');
                    if (string.IsNullOrEmpty(comment) == false) dic.Add("", comment); //class注释
                }

                if (localType.IsEnum)
                {
                    var fields = Enum.GetValues(localType).Cast<Enum>().Select(x => x.ToString()).ToList();
                    foreach (var field in fields)
                    {
                        node = xmlNav.SelectSingleNode($"/doc/members/member[@name='F:{className}.{field}']/summary");
                        if (node != null)
                        {
                            var comment = node.InnerXml.Trim(' ', '\r', '\n', '\t');
                            if (string.IsNullOrEmpty(comment) == false) dic.Add(field, comment); //field注释
                        }
                    }
                }
            }
            return dic;
        }
    }
}

public class ApiModel
{
    /// <summary>
    /// 接口名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 接口地址
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 启用接口日志
    /// </summary>
    public bool EnabledLog { get; set; }
    /// <summary>
    /// 启用请求参数
    /// </summary>
    public bool EnabledParams { get; set; }

    /// <summary>
    /// 启用响应结果
    /// </summary>
    public bool EnabledResult { get; set; }
}