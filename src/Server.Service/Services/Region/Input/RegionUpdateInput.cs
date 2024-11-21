using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Region;


/// <summary>
/// 修改
/// </summary>
public class RegionUpdateInput : RegionAddInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择地区")]
    public long Id { get; set; }
}