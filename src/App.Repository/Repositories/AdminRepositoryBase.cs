
using Framework.Repository.Repositories;

namespace App.Repository.Repositories;

/// <summary>
/// 权限库基础仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class AdminRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class
{
    public AdminRepositoryBase(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.AppDb))
    {

    }
}