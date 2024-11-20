using Server.Repository.Domain;
using Server.Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Server.Repository.Repositories;

public class OrgRepository : AdminRepositoryBase<OrgEntity>, IOrgRepository
{
    public OrgRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }

    /// <summary>
    /// 获得本部门和下级部门Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<long>> GetChildIdListAsync(long id)
    {
        return await Select
        .Where(a => a.Id == id)
        .AsTreeCte()
        .ToListAsync(a => a.Id);
    }
}