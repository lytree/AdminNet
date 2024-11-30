using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public class DictTypeUpdateInput : DictTypeAddInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}