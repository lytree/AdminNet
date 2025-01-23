
using Framework.Repository.Repositories;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Linq.Expressions;

namespace App.Repository.Repositories;

/// <summary>
/// 权限库基础仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class AdminRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class
{
    public IUser User { get; set; }

    public AdminRepositoryBase(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.AppDb))
    {

    }

    public override async Task<bool> SoftDeleteAsync(long id)
    {
        await UpdateDiy
            .SetDto(new
            {
                IsDeleted = true,
                ModifiedUserId = User.Id,
                ModifiedUserName = User.UserName,
                ModifiedUserRealName = User.Name,
                ModifiedTime = DbHelper.ServerTime
            })
        .WhereDynamic(id)
        .ExecuteAffrowsAsync();

        return true;
    }

    public override async Task<bool> SoftDeleteAsync(long[] ids)
    {
        await UpdateDiy
            .SetDto(new
            {
                IsDeleted = true,
                ModifiedUserId = User.Id,
                ModifiedUserName = User.UserName,
                ModifiedUserRealName = User.Name,
                ModifiedTime = DbHelper.ServerTime
            })
            .WhereDynamic(ids)
            .ExecuteAffrowsAsync();

        return true;
    }

    public override async Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
    {
        await UpdateDiy
            .SetDto(new
            {
                IsDeleted = true,
                ModifiedUserId = User.Id,
                ModifiedUserName = User.UserName,
                ModifiedUserRealName = User.Name,
                ModifiedTime = DbHelper.ServerTime
            })
            .Where(exp)
            .DisableGlobalFilter(disableGlobalFilterNames)
            .ExecuteAffrowsAsync();

        return true;
    }

    public override async Task<bool> DeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
    {
        await Select
        .Where(exp)
        .DisableGlobalFilter(disableGlobalFilterNames)
        .AsTreeCte()
        .ToDelete()
        .ExecuteAffrowsAsync();

        return true;
    }

    public override async Task<bool> SoftDeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
    {
        await Select
        .Where(exp)
        .DisableGlobalFilter(disableGlobalFilterNames)
        .AsTreeCte()
        .ToUpdate()
        .SetDto(new
        {
            IsDeleted = true,
            ModifiedUserId = User.Id,
            ModifiedUserName = User.UserName,
            ModifiedUserRealName = User.Name,
            ModifiedTime = DbHelper.ServerTime
        })
        .ExecuteAffrowsAsync();

        return true;
    }
}