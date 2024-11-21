using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Tenant.Dto;

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