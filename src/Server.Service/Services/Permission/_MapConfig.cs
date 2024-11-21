using Mapster;
using System.Linq;
using Server.Service.Domain.Permission;
using Server.Service.Services.Permission.Dto;

namespace Server.Service.Services.Admin.Permission;

/// <summary>
/// 映射配置
/// </summary>
public class MapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
        .NewConfig<PermissionEntity, PermissionGetDotOutput>()
        .Map(dest => dest.ApiIds, src => src.Apis.Select(a => a.Id));
    }
}