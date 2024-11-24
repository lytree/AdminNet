
using App.Core.Dto;
using App.Service.Consts;
using DotNetCore.CAP;
using Framework;
using Plugin.SlideCaptcha;
using Plugin.SlideCaptcha.Validator;
using static Plugin.SlideCaptcha.ValidateResult;

namespace App.Service.Services;

/// <summary>
/// 验证码服务
/// </summary>


public class CaptchaService : BaseService
{
    private readonly ICaptcha _captcha;
    private readonly ISlideCaptcha _slideCaptcha;
    private readonly ICapPublisher _capPublisher;

    public CaptchaService(ICaptcha captcha, ISlideCaptcha slideCaptcha, ICapPublisher capPublisher)
    {
        _captcha = captcha;
        _slideCaptcha = slideCaptcha;
        _capPublisher = capPublisher;
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <returns></returns>


    public CaptchaData Generate(string captchaId = null)
    {
        return _captcha.Generate(captchaId);
    }

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="captchaId">验证码Id</param>
    /// <param name="track">滑动轨迹</param>
    /// <returns></returns>


    public ValidateResult CheckAsync(string captchaId, SlideTrack track)
    {
        if (captchaId.IsNull() || track == null)
        {
            throw ResultOutput.Exception("请完成安全验证");
        }

        return _slideCaptcha.Validate(captchaId, track, false);
    }

    /// <summary>
    /// 发送短信验证码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>


    public async Task<string> SendSmsCodeAsync(SendSmsCodeInput input)
    {
        if (input.Mobile.IsNull())
        {
            throw ResultOutput.Exception("请输入手机号");
        }

        if (input.CaptchaId.IsNull() || input.Track == null)
        {
            throw ResultOutput.Exception("请完成安全验证");
        }

        var validateResult = _captcha.Validate(input.CaptchaId, input.Track);
        if (validateResult.Result != ValidateResultType.Success)
        {
            throw ResultOutput.Exception($"安全{validateResult.Message}");
        }

        var codeId = input.CodeId.IsNull() ? Guid.NewGuid().ToString() : input.CodeId;
        var code = Helper.GenerateRandomNumber();
        await Cache.SetAsync(CacheKeys.GetSmsCodeKey(input.Mobile, codeId), code, TimeSpan.FromMinutes(5));

        //发送短信验证码
        await _capPublisher.PublishAsync(SubscribeNames.SmsSendCode, new
        {
            input.Mobile,
            Text = code
        });

        return codeId;
    }

    /// <summary>
    /// 发送邮件验证码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>


    public async Task<string> SendEmailCodeAsync(SendEmailCodeInput input)
    {
        if (input.Email.IsNull())
        {
            throw ResultOutput.Exception("请输入邮件地址");
        }

        if (input.CaptchaId.IsNull() || input.Track == null)
        {
            throw ResultOutput.Exception("请完成安全验证");
        }

        var validateResult = _captcha.Validate(input.CaptchaId, input.Track);
        if (validateResult.Result != ValidateResultType.Success)
        {
            throw ResultOutput.Exception($"安全{validateResult.Message}");
        }

        var codeId = input.CodeId.IsNull() ? Guid.NewGuid().ToString() : input.CodeId;
        var code = Helper.GenerateRandomNumber();
        await Cache.SetAsync(CacheKeys.GetEmailCodeKey(input.Email, codeId), code, TimeSpan.FromMinutes(5));

        //发送邮箱验证码
        await _capPublisher.PublishAsync(SubscribeNames.EmailSendCode, new EmailSendCodeEvent
        {
            ToEmail = new EmailSendCodeEvent.Models.EmailModel
            {
                Address = input.Email,
            },
            Code = code,
        });

        return codeId;
    }
}