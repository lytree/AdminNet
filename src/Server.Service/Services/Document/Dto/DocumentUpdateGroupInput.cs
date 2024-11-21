using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Document.Dto;

public class DocumentUpdateGroupInput : DocumentAddGroupInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    [ValidateRequired("请选择分组")]
    public long Id { get; set; }
}