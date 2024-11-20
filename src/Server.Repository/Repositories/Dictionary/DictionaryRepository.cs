using Server.Repository.Domain.Dict;

namespace Server.Repository.Repositories.Dictionary;

public class DictionaryRepository : AdminRepositoryBase<DictEntity>, IDictRepository
{
    public DictionaryRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}