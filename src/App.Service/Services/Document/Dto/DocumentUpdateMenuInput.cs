using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

public class DocumentUpdateMenuInput : DocumentAddMenuInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    [ValidateRequired("请选择菜单")]
    public long Id { get; set; }
}