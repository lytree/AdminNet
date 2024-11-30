
using Plugin.SlideCaptcha;
using Plugin.SlideCaptcha.Validator;

namespace App.Service.Services;

public interface ISlideCaptcha
{
    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <param name="slideTrack">滑动轨迹</param>
    /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
    /// <returns></returns>
    ValidateResult Validate(string captchaId, SlideTrack slideTrack, bool removeIfSuccess = true);
}
