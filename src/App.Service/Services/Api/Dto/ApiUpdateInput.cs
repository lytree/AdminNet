using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class ApiUpdateInput : ApiAddInput
{
    /// <summary>
    /// 接口Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}