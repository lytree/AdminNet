

using App.Service.Tools.Cache;
using AspNetCoreRateLimit;

namespace App.Core.Extensions;

public static class IpRateLimitMiddlewares
{
    /// <summary>
    /// 添加Ip限流
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="cacheConfig"></param>
    public static void AddIpRateLimit(this IServiceCollection services, IConfiguration configuration, CacheConfig cacheConfig)
    {
        #region IP限流

        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        if (cacheConfig.TypeRateLimit == CacheType.Redis)
        {
            services.AddDistributedRateLimiting();
        }
        else
        {
            services.AddInMemoryRateLimiting();
        }
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        #endregion IP限流
    }
}
