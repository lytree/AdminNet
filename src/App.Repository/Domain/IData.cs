using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Domain;

/// <summary>
/// 数据权限接口
/// </summary>
public interface IData
{
    /// <summary>
    /// 拥有者Id
    /// </summary>
    long? OwnerId { get; set; }

    /// <summary>
    /// 拥有者部门Id
    /// </summary>
    long? OwnerOrgId { get; set; }

    /// <summary>
    /// 拥有者部门名称
    /// </summary>
    string OwnerOrgName { get; set; }
}