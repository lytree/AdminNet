using System.ComponentModel.DataAnnotations;


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
    public override long Id { get; set; }
}