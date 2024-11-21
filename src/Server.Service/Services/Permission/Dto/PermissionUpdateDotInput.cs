using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Permission.Dto;

public class PermissionUpdateDotInput : PermissionAddDotInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择权限点")]
    public long Id { get; set; }
}