using System.Collections.Generic;

namespace App.Service.Services;

/// <summary>
/// 接口同步
/// </summary>
public class ApiSyncInput
{
    public List<ApiSyncDto> Apis { get; set; }
}