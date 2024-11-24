namespace App.Service.Services;

/// <summary>
/// 设置热门
/// </summary>
public class RegionSetHotInput
{
    /// <summary>
    /// 地区Id
    /// </summary>
    public long RegionId { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool Hot { get; set; }
}