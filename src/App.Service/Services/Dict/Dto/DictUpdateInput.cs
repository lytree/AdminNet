using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public class DictUpdateInput : DictAddInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择数据字典")]
    public long Id { get; set; }
}