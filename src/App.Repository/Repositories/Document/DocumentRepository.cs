using App.Repository.Domain.Document;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class DocumentRepository : AdminRepositoryBase<DocumentEntity>, IDocumentRepository
{
    public DocumentRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}