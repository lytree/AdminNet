﻿using App.Repository.Domain;
using App.Service.Domain;

namespace App.Service.Services;

public class DocumentAddMenuInput
{
    /// <summary>
    /// 父级节点
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public DocumentType Type { get; set; }

    /// <summary>
    /// 命名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }
}