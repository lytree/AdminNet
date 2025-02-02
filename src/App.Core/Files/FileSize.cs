﻿using Framework.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Files;


/// <summary>
/// 文件大小
/// </summary>
public struct FileSize
{
    /// <summary>
    /// 初始化文件大小
    /// </summary>
    /// <param name="size">文件大小</param>
    /// <param name="unit">文件大小单位</param>
    public FileSize(long size, FileSizeUnit unit = FileSizeUnit.Byte)
    {
        switch (unit)
        {
            case FileSizeUnit.K:
                Size = size * 1024; break;
            case FileSizeUnit.M:
                Size = size * 1024 * 1024; break;
            case FileSizeUnit.G:
                Size = size * 1024 * 1024 * 1024; break;
            default:
                Size = size; break;
        }
    }

    /// <summary>
    /// 文件字节长度
    /// </summary>
    public long Size { get; }

    /// <summary>
    /// 获取文件大小，单位：字节
    /// </summary>
    public long GetSize()
    {
        return Size;
    }

    /// <summary>
    /// 获取文件大小，单位：K
    /// </summary>
    public double GetSizeByK()
    {
        return Math.Round((Size / 1024.0).ConvertTo<double>(),2);
    }

    /// <summary>
    /// 获取文件大小，单位：M
    /// </summary>
    public double GetSizeByM()
    {
        return Math.Round((Size / 1024.0 / 1024.0).ConvertTo<double>(), 2);
    }

    /// <summary>
    /// 获取文件大小，单位：G
    /// </summary>
    public double GetSizeByG()
    {
        return Math.Round((Size / 1024.0 / 1024.0 / 1024.0).ConvertTo<double>(), 2);
    }

    /// <summary>
    /// 输出描述
    /// </summary>
    public override string ToString()
    {
        if (Size >= 1024 * 1024 * 1024)
            return $"{GetSizeByG()} {FileSizeUnit.G.GetDescription()}";
        if (Size >= 1024 * 1024)
            return $"{GetSizeByM()} {FileSizeUnit.M.GetDescription()}";
        if (Size >= 1024)
            return $"{GetSizeByK()} {FileSizeUnit.K.GetDescription()}";
        return $"{Size} {FileSizeUnit.Byte.GetDescription()}";
    }
}