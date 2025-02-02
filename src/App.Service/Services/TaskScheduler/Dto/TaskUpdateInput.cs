﻿using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class TaskUpdateInput : TaskAddInput
{
    /// <summary>
    /// 任务Id
    /// </summary>
    [Required(ErrorMessage = "请选择任务")]
    public string Id { get; set; }
}