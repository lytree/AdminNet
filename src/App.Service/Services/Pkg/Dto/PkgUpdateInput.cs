using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class PkgUpdateInput : PkgAddInput
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}