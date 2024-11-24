
using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

public class FileDeleteInput
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择文件")]
    public long Id { get; set; }
}