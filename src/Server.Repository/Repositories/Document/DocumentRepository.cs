using Server.Repository.Domain.Document;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class DocumentRepository : AdminRepositoryBase<DocumentEntity>, IDocumentRepository
{
    public DocumentRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}