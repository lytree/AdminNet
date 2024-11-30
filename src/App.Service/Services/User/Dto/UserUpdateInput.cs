using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class UserUpdateInput: UserFormInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public override long Id { get; set; }
}