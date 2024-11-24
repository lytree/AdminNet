using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qiniu.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Service;


internal static class AppInfoBase
{
    internal static IServiceCollection Services;

    internal static IServiceProvider ServiceProvider;

    internal static IWebHostEnvironment WebHostEnvironment;

    internal static IHostEnvironment HostEnvironment;

    internal static IConfiguration Configuration;

    internal static HostInfo HostInfo;

    internal static void ConfigureApplication(this WebApplicationBuilder webApplicationBuilder, Assembly assembly)
    {
        HostEnvironment = webApplicationBuilder.Environment;
        WebHostEnvironment = webApplicationBuilder.Environment;
        Services = webApplicationBuilder.Services;
        Configuration = webApplicationBuilder.Configuration;
        HostInfo = HostInfo.CreateInstance(assembly);
    }

    internal static void ConfigureApplication(this WebApplication app)
    {
        ServiceProvider = app.Services;
    }
}