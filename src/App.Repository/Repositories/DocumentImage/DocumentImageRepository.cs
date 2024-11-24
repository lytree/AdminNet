using App.Repository.Domain.DocumentImage;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class DocumentImageRepository : AdminRepositoryBase<DocumentImageEntity>, IDocumentImageRepository
{
    public DocumentImageRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}