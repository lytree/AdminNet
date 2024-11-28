using System.ComponentModel.DataAnnotations;

namespace App.Service.Services;

public class DocumentUpdateContentInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Html
    /// </summary>
    public string Html { get; set; }
}