using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public class ViewUpdateInput : ViewAddInput
{
    /// <summary>
    /// 视图Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}