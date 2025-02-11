﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class PermissionAssignInput
{
    [Required(ErrorMessage = "角色不能为空")]
    public long RoleId { get; set; }

    [Required(ErrorMessage = "权限不能为空")]
    public List<long> PermissionIds { get; set; }
}