﻿using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

/// <summary>
/// 更新基本信息
/// </summary>
public class UserUpdateBasicInput
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "请输入姓名")]
    public string Name { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
}