
using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class FileDeleteInput
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}