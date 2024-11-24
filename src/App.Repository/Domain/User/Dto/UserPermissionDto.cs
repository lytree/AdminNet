using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Domain;

/// <summary>
/// 用户权限
/// </summary>
public class UserPermissionDto
{
    public static class Models
    {
        /// <summary>
        /// 接口
        /// </summary>
        public class ApiModel
        {
            /// <summary>
            /// 请求方法
            /// </summary>
            public string HttpMethods { get; set; }

            /// <summary>
            /// 请求地址
            /// </summary>
            public string Path { get; set; }
        }
    }

    /// <summary>
    /// 接口列表
    /// </summary>
    public List<Models.ApiModel> Apis { get; set; }

    /// <summary>
    /// 权限点编码列表
    /// </summary>
    public List<string> Codes { get; set; }
}
