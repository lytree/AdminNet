using Server.Repository.Domain;
using Server.Repository.Repositories;

namespace Server.Repository.Repositories;

public class DictionaryTypeRepository : AdminRepositoryBase<DictTypeEntity>, IDictTypeRepository
{
    public DictionaryTypeRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}