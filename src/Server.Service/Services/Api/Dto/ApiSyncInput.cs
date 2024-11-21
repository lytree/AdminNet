using System.Collections.Generic;

namespace Server.Service.Services.Api.Dto;

/// <summary>
/// 接口同步
/// </summary>
public class ApiSyncInput
{
    public List<ApiSyncDto> Apis { get; set; }
}