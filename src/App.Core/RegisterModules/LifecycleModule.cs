using App.Core.Attributes;
using Autofac;
using Framework;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace App.Core.RegisterModules;

/// <summary>
/// 生命周期注入
/// </summary>
public class LifecycleModule : Module
{
    private readonly string[] _assemblyNames;

    public LifecycleModule(params string[] assemblyNames)
    {
        _assemblyNames = assemblyNames;
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_assemblyNames?.Length > 0)
        {
            // 获得要注入的程序集
            Assembly[] assemblies = Helper.GetAssemblyList(_assemblyNames);

            //无接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectSingletonAttribute>(false) != null)
            .SingleInstance()
            .PropertiesAutowired();

            //有接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectSingletonAttribute>(false) != null)
            .AsImplementedInterfaces()
            .SingleInstance()
            .PropertiesAutowired();

            //无接口注入作用域
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectScopedAttribute>(false) != null)
            .InstancePerLifetimeScope()
            .PropertiesAutowired();

            //有接口注入作用域
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectScopedAttribute>(false) != null)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .PropertiesAutowired();

            //无接口注入瞬时
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectTransientAttribute>(false) != null)
            .InstancePerDependency()
            .PropertiesAutowired();

            //有接口注入瞬时
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<InjectTransientAttribute>(false) != null)
            .AsImplementedInterfaces()
            .InstancePerDependency()
            .PropertiesAutowired();
        }
    }
}
