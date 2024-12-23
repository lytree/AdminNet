﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class PkgSetPkgPermissionsInput
{
    [Required(ErrorMessage = "套餐不能为空")]
    public long PkgId { get; set; }

    [Required(ErrorMessage = "权限不能为空")]
    public List<long> PermissionIds { get; set; }
}