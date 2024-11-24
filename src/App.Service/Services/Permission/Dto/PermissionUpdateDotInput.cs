using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

namespace App.Service.Services;

public class PermissionUpdateDotInput : PermissionAddDotInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择权限点")]
    public long Id { get; set; }
}