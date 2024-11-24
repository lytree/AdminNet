using System.ComponentModel;

namespace App.Service.Consts;

/// <summary>
/// 订阅命名
/// </summary>
public partial class SubscribeNames
{
    /// <summary>
    /// 短信单发
    /// </summary>
    [Description("短信单发")]
    public const string SmsSingleSend = "zhontai.admin.sms:singleSend";

    /// <summary>
    /// 短信验证码发送
    /// </summary>
    [Description("短信验证码发送")]
    public const string SmsSendCode = "zhontai.admin.sms:sendCode";

    /// <summary>
    /// 邮件单发
    /// </summary>
    [Description("邮件单发")]
    public const string EmailSingleSend = "zhontai.admin.email:singleSend";

    /// <summary>
    /// 邮箱验证码发送
    /// </summary>
    [Description("邮箱验证码发送")]
    public const string EmailSendCode = "zhontai.admin.email:sendCode";
}