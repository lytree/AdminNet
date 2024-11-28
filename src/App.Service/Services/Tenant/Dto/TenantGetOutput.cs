
using App.Repository.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace App.Service.Services;

public class TenantGetOutput : TenantUpdateInput
{
    /// <summary>
    /// 套餐列表
    /// </summary>
    [JsonIgnore]
    public ICollection<PkgEntity> Pkgs { get; set; }

    /// <summary>
    /// 套餐Ids
    /// </summary>
    public override long[] PkgIds => Pkgs?.Select(a => a.Id)?.ToArray();
}