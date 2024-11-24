using App.Repository;
using Framework.Repository.Attributes;
using System;

namespace App.Service.Attributes;

/// <summary>
/// 启用权限库事务
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class AdminTransactionAttribute : TransactionAttribute
{
    public AdminTransactionAttribute() : base(DbKeys.AppDb)
    {
    }
}