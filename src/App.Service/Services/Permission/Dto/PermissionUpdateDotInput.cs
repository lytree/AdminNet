using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

public class PermissionUpdateDotInput : PermissionAddDotInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}