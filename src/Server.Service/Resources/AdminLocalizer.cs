using Microsoft.Extensions.Localization;

namespace Server.Service.Resources;

/// <summary>
/// Admin国际化
/// </summary>
public class AdminLocalizer: ModuleLocalizer
{
    public AdminLocalizer(IStringLocalizer<AdminLocalizer> localizer) : base(localizer)
    {
    }
}
