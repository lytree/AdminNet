using Server.Repository.Domain.DocumentImage;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class DocumentImageRepository : AdminRepositoryBase<DocumentImageEntity>, IDocumentImageRepository
{
    public DocumentImageRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}