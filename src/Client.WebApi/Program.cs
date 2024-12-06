
using App.Core.Configs;
using App.Core.RegisterModules;
using App.Core.Startup;
using App.Repository;
using App.Service;
using App.Service.Resources;
using AspNetCoreRateLimit;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Client.WebApi.Middlewares;
using Client.WebApi.Routes;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Scalar.AspNetCore;
using System.Reflection;
namespace Client.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var services = builder.Services;
            var env = builder.Environment;
            var configuration = builder.Configuration;

            var host = new HostApp(new HostAppOptions { });
            host.ConfigureApplication(builder, Assembly.GetCallingAssembly());
            //�������
            configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            if (env.EnvironmentName.NotNull())
            {
                configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }




            // Add services to the container.
            services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddOpenApiDocument();
            //ʹ��Autofac����
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //����Autofac����
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                // ��������ע��
                builder.RegisterModule(new LifecycleModule([]));

                // ������ע��
                //builder.RegisterModule(new ControllerModule());

                // ģ��ע��
                builder.RegisterModule(new RepositoryRegisterModule());
                // ģ��ע��
                builder.RegisterModule(new ServiceRegisterModule());
                //_hostAppOptions?.ConfigureAutofacContainer?.Invoke(builder, hostAppContext);
            });


            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            ////load general configuration from appsettings.json
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            ////load ip rules from appsettings.json
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddInMemoryRateLimiting();
            //services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
            //services.AddDistributedRateLimiting<RedisProcessingStrategy>();
            //services.AddRedisRateLimiting();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            var appConfig = AppInfo.GetOptions<AppConfig>();
            services.AddSingleton(appConfig);
            services.AddSingleton<AdminLocalizer>();
            var app = builder.Build();
            var ser = app.Services;
            host.ConfigureApplication(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi(options =>
                {
                    options.Path = "/openapi/{documentName}.json";
                });
                app.MapScalarApiReference();
            }





            app.UseRouting();


            app.UseStaticFiles();
            app.ApiEndpoints();
            app.UseMiddleware<ExceptionMiddleware>();

            //app.UseCors();

            //app.UseAuthorization();
            //app.UseAuthentication();
            //app.UseAuthorization();




            app.UseIpRateLimiting();




            app.Run();
        }
    }
}
