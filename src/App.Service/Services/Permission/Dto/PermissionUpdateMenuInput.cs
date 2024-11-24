using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

public class PermissionUpdateMenuInput : PermissionAddMenuInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择菜单")]
    public long Id { get; set; }
}