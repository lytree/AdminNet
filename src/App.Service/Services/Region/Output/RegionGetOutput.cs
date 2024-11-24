using System.Collections.Generic;

namespace App.Service.Services;

public class RegionGetOutput : RegionUpdateInput
{
    public List<long> ParentIdList { get; set; }
}