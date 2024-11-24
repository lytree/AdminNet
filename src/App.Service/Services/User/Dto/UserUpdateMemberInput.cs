using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

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
    [ValidateRequired("请选择会员")]
    public override long Id { get; set; }
}