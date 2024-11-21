using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Dict.Dto;

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