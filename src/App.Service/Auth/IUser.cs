

using App.Repository.Domain;

namespace Server.Core.Auth;

/// <summary>
/// 用户信息接口
/// </summary>
public interface IUser : Framework.Repository.IUser
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public UserType Type { get; }

    /// <summary>
    /// 默认用户
    /// </summary>
    public bool DefaultUser { get; }

    /// <summary>
    /// 平台管理员
    /// </summary>
    public bool PlatformAdmin { get; }

    /// <summary>
    /// 租户管理员
    /// </summary>
    public bool TenantAdmin { get; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long? TenantId { get; }

    /// <summary>
    /// 租户类型
    /// </summary>
    public TenantType? TenantType { get; }

    /// <summary>
    /// 数据库注册键
    /// </summary>
    public string DbKey { get; }

    /// <summary>
    /// 数据权限
    /// </summary>
    public DataPermissionDto DataPermission { get; }

    /// <summary>
    /// 用户权限
    /// </summary>
    public UserPermissionDto UserPermission { get; }

    /// <summary>
    /// 检查用户是否拥有某个权限点
    /// </summary>
    /// <param name="permissionCode">权限点编码</param>
    /// <returns></returns>
    public bool HasPermission(string permissionCode);

    /// <summary>
    /// 检查用户是否拥有这些权限点
    /// </summary>
    /// <param name="permissionCodes">权限点编码列表</param>
    /// <param name="all">是否全部满足</param>
    /// <returns></returns>
    public bool HasPermissions(string[] permissionCodes, bool all = false);
}