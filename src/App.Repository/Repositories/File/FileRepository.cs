using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class FileRepository : AdminRepositoryBase<FileEntity>, IFileRepository
{
    public FileRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}