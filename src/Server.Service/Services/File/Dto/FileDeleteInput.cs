
using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Dto;

public class FileDeleteInput
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择文件")]
    public long Id { get; set; }
}