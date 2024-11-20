using FreeSql.DataAnnotations;

namespace Server.Repository.Domain;

/// <summary>
/// 登录日志
/// </summary>
[Table(Name = "ad_login_log")]
public partial class LoginLogEntity : LogAbstract
{
}