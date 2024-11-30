using App.Core.Configs;
using App.Service.Consts;
using DotNetCore.CAP;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace App.Service.Services;

public class EmailService: ICapSubscribe
{
    private readonly IOptions<EmailConfig> _emailConfig;

    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig;
    }

    /// <summary>
    /// 邮件单发
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>

    [CapSubscribe(SubscribeNames.EmailSingleSend)]
    public async Task SingleSendAsync(EmailSingleSendEvent @event)
    {
        var emailConfig = _emailConfig.Value;

        var builder = new BodyBuilder()
        {
            HtmlBody = @event.Body
        };
        var message = new MimeMessage()
        {
            Subject = @event.Subject,
            Body = builder.ToMessageBody()
        };

        var fromEmailName = @event.FromEmail!=null && @event.FromEmail.Name.NotNull() ? @event.FromEmail.Name : emailConfig.FromEmail.Name;
        var fromEmailAddress = @event.FromEmail != null && @event.FromEmail.Address.NotNull() ? @event.FromEmail.Address : emailConfig.FromEmail.Address;
        message.From.Add(new MailboxAddress(fromEmailName, fromEmailAddress));
        message.To.Add(new MailboxAddress(@event.ToEmail.Name, @event.ToEmail.Address));

        using var client = new SmtpClient();
        await client.ConnectAsync(emailConfig.Host, emailConfig.Port, emailConfig.UseSsl);
        await client.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>

    [CapSubscribe(SubscribeNames.EmailSendCode)]
    public async Task SendCodeAsync(EmailSendCodeEvent @event)
    {
        await SingleSendAsync(new EmailSingleSendEvent
        {
            ToEmail = new EmailSingleSendEvent.Models.EmailModel
            {
                Address = @event.ToEmail.Address,
            },
            Subject = "邮箱验证码",
            Body = $@"<p>你正在进行邮箱登录操作</p>
<p>邮箱验证码: {@event.Code}，有效期5分钟</p>
<p>如非本人操作，请忽略。</p>"
        });
    }
}
