using App.Core.Attributes;
using App.Core.Configs;
using App.Core.Handlers;

namespace App.Core.Filters;

/// <summary>
/// 控制器操作日志记录
/// </summary>
public class ControllerLogFilter 
{
    private readonly AppConfig _appConfig;

    public ControllerLogFilter(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Items["_ActionArguments"] = context.ActionArguments;

        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NoOperationLogAttribute)) || !_appConfig.Log.Operation)
        {
            await next();
            return;
        }

        await AppInfo.GetRequiredService<ILogHandler>().LogAsync(context, next);
    }
}