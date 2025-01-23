using App.Core;
using App.Core.Configs;
using App.Core.Extensions;
using App.Core.RegisterModules;
using App.Core.Startup;
using App.Repository;
using App.Service.Extensions;
using App.Service.Helpers;

using App.Service.Services;
using App.Service.Tools.Cache;
using Autofac;
using FreeRedis;
using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin.SlideCaptcha;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.Json.Serialization;
using Yitter.IdGenerator;
using App.Service.Consts;
using Framework;

namespace App.Service;

public class HostApp
{
    readonly HostAppOptions _hostAppOptions;

    /// <summary>
    /// 添加配置文件
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="environmentName">环境名</param>
    /// <param name="directory">目录</param>
    /// <param name="optional">可选</param>
    /// <param name="reloadOnChange">热更新</param>
    private static void AddJsonFilesFromDirectory(
        ConfigurationManager configuration,
        string environmentName,
        string directory = "ConfigCenter",
        bool optional = true,
        bool reloadOnChange = true)
    {
        var allFilePaths = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, directory).ToPath())
            .Where(p => p.EndsWith($".json", StringComparison.OrdinalIgnoreCase));

        var environmentFilePaths = allFilePaths.Where(p => p.EndsWith($".{environmentName}.json", StringComparison.OrdinalIgnoreCase));
        var otherFilePaths = allFilePaths.Except(environmentFilePaths);
        var filePaths = otherFilePaths.Concat(environmentFilePaths);

        foreach (var filePath in filePaths)
        {
            configuration.AddJsonFile(filePath, optional: optional, reloadOnChange: reloadOnChange);
        }

    }
    public HostApp()
    {
    }

    public HostApp(HostAppOptions hostAppOptions)
    {
        _hostAppOptions = hostAppOptions;
    }

}
