using System.Collections.Generic;

namespace Server.Service.Services.Region;

public class RegionGetOutput : RegionUpdateInput
{
    public List<long> ParentIdList { get; set; }
}