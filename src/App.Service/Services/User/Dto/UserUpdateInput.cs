using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

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
    [ValidateRequired("请选择用户")]
    public override long Id { get; set; }
}