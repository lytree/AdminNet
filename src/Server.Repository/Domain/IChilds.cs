﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository.Domain;

/// <summary>
/// 子级接口
/// </summary>
public interface IChilds<T>
{
    List<T> Childs { get; set; }
}
