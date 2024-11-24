using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

public class PermissionUpdateGroupInput : PermissionAddGroupInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择分组")]
    public long Id { get; set; }
}