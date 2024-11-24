using App.Repository.Domain;

namespace App.Repository.Repositories.Dictionary;

public class DictionaryRepository : AdminRepositoryBase<DictEntity>, IDictRepository
{
    public DictionaryRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}