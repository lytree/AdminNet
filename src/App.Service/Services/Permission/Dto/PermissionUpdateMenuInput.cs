using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

public class PermissionUpdateMenuInput : PermissionAddMenuInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}