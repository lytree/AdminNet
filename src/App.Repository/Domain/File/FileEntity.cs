﻿using Framework.Repository.Entities;
using FreeSql.DataAnnotations;
using Framework.Repository.Attributes;
using System;
using Framework.OSS;

namespace App.Repository.Domain;

/// <summary>
/// 文件
/// </summary>
[Table(Name = "ad_file")]
public partial class FileEntity : EntityBase
{
    /// <summary>
    /// OSS供应商
    /// </summary>
    [Column(MapType = typeof(string), StringLength = 50)]
    public OSSProvider? Provider { get; set; }

    /// <summary>
    /// 存储桶名称
    /// </summary>
    [Column(StringLength = 200)]
    public string BucketName { get; set; }

    /// <summary>
    /// 文件目录
    /// </summary>
    [Column(StringLength = 500)]
    public string FileDirectory { get; set; }

    /// <summary>
    /// 文件Guid
    /// </summary>
    
    public Guid FileGuid { get; set; }

    /// <summary>
    /// 保存文件名
    /// </summary>
    [Column(StringLength = 200)]
    public string SaveFileName { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    [Column(StringLength = 200)]
    public string FileName { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [Column(StringLength = 20)]
    public string Extension { get; set; }

    /// <summary>
    /// 文件字节长度
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 文件大小格式化
    /// </summary>
    [Column(StringLength = 50)]
    public string SizeFormat { get; set; }

    /// <summary>
    /// 链接地址
    /// </summary>
    [Column(StringLength = 500)]
    public string LinkUrl { get; set; }

    /// <summary>
    /// md5码，防止上传重复文件
    /// </summary>
    [Column(StringLength = 50)]
    public string Md5 { get; set; } = string.Empty;
}