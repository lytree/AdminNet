using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class DocumentUpdateGroupInput : DocumentAddGroupInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    public long Id { get; set; }
}