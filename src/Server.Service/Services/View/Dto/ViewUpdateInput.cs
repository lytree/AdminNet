using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.View.Dto;

/// <summary>
/// 修改
/// </summary>
public class ViewUpdateInput : ViewAddInput
{
    /// <summary>
    /// 视图Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择视图")]
    public long Id { get; set; }
}