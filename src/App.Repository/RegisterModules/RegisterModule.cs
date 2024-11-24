using Autofac;
using System.Reflection;
using Module = Autofac.Module;
using App.Core.Attributes;
using Framework;
using App.Repository;
using CommunityToolkit.HighPerformance.Buffers;
using Framework.Repository.Repositories;
using App.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using App.Core.RegisterModules;
using Autofac.Extras.DynamicProxy;

namespace App.Repository.RegisterModules;

public class RegisterModule : Module
{
    private readonly string[] _assemblyNames;
    private readonly bool _isTransaction;
    /// <summary>
    /// 模块注入
    /// </summary>
    /// <param name="assemblyNames">string[]</param>
    public RegisterModule(bool isTransaction = false, params string[] assemblyNames)
    {
        _assemblyNames = assemblyNames;
    }

    protected override void Load(ContainerBuilder builder)
    {
        //事务拦截
        var interceptorServiceTypes = new List<Type>();
        if (_isTransaction)
        {
            builder.RegisterType<TransactionInterceptor>();
            builder.RegisterType<TransactionAsyncInterceptor>();
            interceptorServiceTypes.Add(typeof(TransactionInterceptor));
        }

        if (_assemblyNames?.Length > 0)
        {
            //程序集
            Assembly[] assemblies = Helper.GetAssemblyList(_assemblyNames);

            static bool Predicate(Type a) => !a.IsDefined(typeof(NonRegisterIOCAttribute), true)
                && (a.Name.EndsWith("Service") || a.Name.EndsWith("Repository") || typeof(IRegisterIOC).IsAssignableFrom(a))
                && !a.IsAbstract && !a.IsInterface && a.IsPublic;

            //有接口实例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(new Func<Type, bool>(Predicate))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .PropertiesAutowired()// 属性注入
            .InterceptedBy(interceptorServiceTypes.ToArray())
            .EnableInterfaceInterceptors();

            //无接口实例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(new Func<Type, bool>(Predicate))
            .InstancePerLifetimeScope()
            .PropertiesAutowired()// 属性注入
            .InterceptedBy(interceptorServiceTypes.ToArray())
            .EnableClassInterceptors();

            //密码哈希泛型注入
            builder.RegisterGeneric(typeof(PasswordHasher<>)).As(typeof(IPasswordHasher<>)).SingleInstance().PropertiesAutowired();

            //仓储泛型注入
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepositoryBase<,>)).InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(AdminRepositoryBase<>)).InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}
