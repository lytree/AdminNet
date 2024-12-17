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


    /// <summary>
    /// 运行应用
    /// </summary>
    /// <param name="args"></param>
    /// <param name="assembly"></param>
    public void Run(WebApplicationBuilder builder, Assembly assembly = null)
    {


        _hostAppOptions?.ConfigurePreWebApplicationBuilder?.Invoke(builder);


        builder.ConfigureApplication(assembly ?? Assembly.GetCallingAssembly());


        //清空日志供应程序，避免.net自带日志输出到命令台
        builder.Logging.ClearProviders();


        var services = builder.Services;
        var env = builder.Environment;
        var configuration = builder.Configuration;



        //添加配置
        configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        if (env.EnvironmentName.NotNull())
        {
            configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json" ,optional: true ,reloadOnChange: true);
        }

        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        var appSettings = AppInfo.GetOptions<AppSettings>();

        if (appSettings.UseConfigCenter)
        {
            AddJsonFilesFromDirectory(configuration ,env.EnvironmentName, appSettings.ConfigCenterPath);
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            services.Configure<DbConfig>(configuration.GetSection("DbConfig"));
            services.Configure<CacheConfig>(configuration.GetSection("CacheConfig"));
            services.Configure<OSSConfig>(configuration.GetSection("OssConfig"));
        }
        else
        {
            //app应用配置
            services.Configure<AppConfig>(ConfigHelper.Load("appconfig", env.EnvironmentName));

            //jwt配置
            services.Configure<JwtConfig>(ConfigHelper.Load("jwtconfig", env.EnvironmentName));

            //数据库配置
            services.Configure<DbConfig>(ConfigHelper.Load("dbconfig", env.EnvironmentName));

            //缓存配置
            services.Configure<CacheConfig>(ConfigHelper.Load("cacheconfig", env.EnvironmentName));
            
            //oss上传配置
            services.Configure<OSSConfig>(ConfigHelper.Load("ossconfig", env.EnvironmentName));


            //限流配置
            configuration.AddJsonFile("./Configs/ratelimitconfig.json", optional: true, reloadOnChange: true);
            if (env.EnvironmentName.NotNull())
            {
                configuration.AddJsonFile($"./Configs/ratelimitconfig.{env.EnvironmentName}.json" ,optional: true, reloadOnChange: true);
            }
        }

        services.Configure<EmailConfig>(configuration.GetSection("Email"));



        //app应用配置
        var appConfig = AppInfo.GetOptions<AppConfig>();
        services.AddSingleton(appConfig);

        //jwt配置
        services.AddSingleton(AppInfo.GetOptions<JwtConfig>());

        //数据库配置
        services.AddSingleton(AppInfo.GetOptions<DbConfig>());

        //缓存配置
        services.AddSingleton(AppInfo.GetOptions<CacheConfig>());

        var hostAppContext = new HostAppContext()
        {
            Services = services,
            Environment = env,
            Configuration = configuration,
        };
        //使用Autofac容器
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


        //配置Autofac容器
        builder.Host.ConfigureContainer<ContainerBuilder>((context ,builder) =>
        {
            // 生命周期注入
            builder.RegisterModule(new LifecycleModule(appConfig.AssemblyNames));

            // 模块注入
            builder.RegisterModule(new RepositoryRegisterModule());
            // 模块注入
            builder.RegisterModule(new ServiceRegisterModule());

            _hostAppOptions?.ConfigureAutofacContainer?.Invoke(builder, hostAppContext);
        });



    }


    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="env"></param>
    /// <param name="configuration"></param>
    /// <param name="appConfig"></param>
    private void ConfigureServices(IServiceCollection services ,IWebHostEnvironment env ,IConfiguration configuration ,AppConfig appConfig)
    {
        var hostAppContext = new HostAppContext()
        {
            Services = services,
            Environment = env,
            Configuration = configuration
        };


        _hostAppOptions?.ConfigurePreServices?.Invoke(hostAppContext);

        //健康检查
        services.AddHealthChecks();

        var cacheConfig = AppInfo.GetOptions<CacheConfig>();

        #region 缓存
        //添加内存缓存
        services.AddMemoryCache();
        if (cacheConfig.Type == CacheType.Redis)
        {
            //FreeRedis客户端
            var redis = new RedisClient(cacheConfig.Redis.ConnectionString)
            {
                Serialize = JsonConvert.SerializeObject,
                Deserialize = JsonConvert.DeserializeObject
            };
            services.AddSingleton(redis);
            services.AddSingleton<IRedisClient>(redis);
            //Redis缓存
            services.AddSingleton<ICacheTool ,RedisCacheTool>();
            //分布式Redis缓存
            services.AddSingleton<IDistributedCache>(new DistributedCache(redis));
            if (_hostAppOptions?.ConfigureIdGenerator != null)
            {
                _hostAppOptions?.ConfigureIdGenerator?.Invoke(appConfig.IdGenerator);
                YitIdHelper.SetIdGenerator(appConfig.IdGenerator);
            }
            else
            {
                //分布式Id生成器
                services.AddIdGenerator();
            }
        }
        else
        {
            //内存缓存
            services.AddSingleton<ICacheTool ,MemoryCacheTool>();
            //分布式内存缓存
            services.AddDistributedMemoryCache();
            //Id生成器
            _hostAppOptions?.ConfigureIdGenerator?.Invoke(appConfig.IdGenerator);
            YitIdHelper.SetIdGenerator(appConfig.IdGenerator);
        }

        #endregion 缓存

        //权限处理
        services.AddScoped<IPermissionHandler, PermissionHandler>();

        // ClaimType不被更改
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        //用户信息
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddScoped<IUser, User>();

        //添加数据库
        if (!_hostAppOptions.CustomInitDb)
        {
            services.AddDb(env ,_hostAppOptions);
        }

        //程序集
        Assembly[] assemblies = Helper.GetAssemblyList(appConfig.AssemblyNames);

        #region Mapster 映射配置
        services.AddScoped<IMapper>(sp => new Mapper());
        if (assemblies?.Length > 0)
        {
            TypeAdapterConfig.GlobalSettings.Scan(assemblies);
        }
        #endregion Mapster 映射配置


        #region 操作日志

        services.AddScoped<ILogHandler, LogHandler>();

        #endregion 操作日志

        services.AddHttpClient();

        _hostAppOptions?.ConfigureServices?.Invoke(hostAppContext);


        //性能分析
        if (appConfig.MiniProfiler)
        {
            services.AddMiniProfiler();
        }

        //oss文件上传
        services.AddOSS();

        //滑块验证码
        services.AddSlideCaptcha(configuration ,options =>
        {
            options.StoreageKeyPrefix = CacheKeys.Captcha;
        });
        services.AddScoped<ISlideCaptcha, SlideCaptcha>();

        //IP地址定位库
        if (appConfig.IP2Region.Enable)
        {
            services.AddSingleton<ISearcher>(new Searcher(CachePolicy.Content, Path.Combine(AppContext.BaseDirectory, "ip2region.xdb")));
        }

        _hostAppOptions?.ConfigurePostServices?.Invoke(hostAppContext);
    }
}
