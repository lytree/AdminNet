using App.Core.Configs;
using App.Core.Startup;
using App.Repository;
using App.Repository.Repositories;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Extensions;

public static class DBServiceCollectionExtensions
{
    /// <summary>
    /// 添加数据库
    /// </summary>
    /// <param name="services"></param>
    /// <param name="env"></param>
    /// <param name="hostAppOptions"></param>
    /// <returns></returns>
    public static void AddDb(this IServiceCollection services, IHostEnvironment env, HostAppOptions hostAppOptions)
    {
        var dbConfig = AppInfo.GetOptions<DbConfig>();
        var appConfig = AppInfo.GetOptions<AppConfig>();
        var user = services.BuildServiceProvider().GetService<IUser>();
        var freeSqlCloud = appConfig.DistributeKey.IsNull() ? new FreeSqlCloud() : new FreeSqlCloud(appConfig.DistributeKey);
        DbHelper.RegisterDb(freeSqlCloud, user, dbConfig, hostAppOptions.ConfigureFreeSqlBuilder, hostAppOptions.ConfigurePreFreeSql,
        hostAppOptions.ConfigureFreeSqlSyncStructure,
        hostAppOptions.ConfigureFreeSql);

        //运行主库
        var masterDb = freeSqlCloud.Use(dbConfig.Key);
        services.AddSingleton(provider => masterDb);
        masterDb.Select<object>();

        //注册多数据库
        if (dbConfig.Dbs?.Length > 0)
        {
            foreach (var db in dbConfig.Dbs)
            {
                DbHelper.RegisterDb(freeSqlCloud, user, dbConfig, hostAppOptions.ConfigureFreeSqlBuilder, hostAppOptions.ConfigurePreFreeSql,
                hostAppOptions.ConfigureFreeSqlSyncStructure,
                hostAppOptions.ConfigureFreeSql);
                //运行当前库
                var currentDb = freeSqlCloud.Use(db.Key);
                currentDb.Select<object>();
            }
        }

        services.AddSingleton<IFreeSql>(freeSqlCloud);
        services.AddSingleton(freeSqlCloud);
        services.AddScoped<UnitOfWorkManagerCloud>();
    }
}
