using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Domain;

/// <summary>
/// 会员接口
/// </summary>
public interface IMember
{
    /// <summary>
    /// 顾客Id
    /// </summary>
    long? MemberId { get; set; }
}
