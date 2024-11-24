using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class TenantUpdateInput : TenantAddInput
{
    /// <summary>
    /// 租户Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择租户")]
    public override long Id { get; set; }
}