using App.Repository.Domain;
using App.Repository.Repositories;

namespace App.Repository.Repositories;

public class DictionaryTypeRepository : AdminRepositoryBase<DictTypeEntity>, IDictTypeRepository
{
    public DictionaryTypeRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}