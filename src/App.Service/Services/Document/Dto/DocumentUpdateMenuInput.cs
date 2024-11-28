using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class DocumentUpdateMenuInput : DocumentAddMenuInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    public long Id { get; set; }
}