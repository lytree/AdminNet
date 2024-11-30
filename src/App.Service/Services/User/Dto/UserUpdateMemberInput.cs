using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改会员
/// </summary>
public class UserUpdateMemberInput: UserMemberFormInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public override long Id { get; set; }
}