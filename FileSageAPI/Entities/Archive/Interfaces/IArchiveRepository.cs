using FileSageAPI.Database.Models;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Archive.Interfaces
{
    public interface IArchiveRepository
    {
        Task<List<ArchiveModel>> Get();
        Task<ArchiveModel?> Get(int id);
        Task<List<ArchiveModel>> Get(Expression<Func<ArchiveModel, bool>> predicate);
        Task<ArchiveModel?> Get(int id, Expression<Func<ArchiveModel, bool>> predicate);
        Task<bool> Exists(Expression<Func<ArchiveModel, bool>> predicate);
        Task<ArchiveModel> Insert(ArchiveModel entity);
        Task<ArchiveModel> Update(ArchiveModel entity);
        Task<ArchiveModel> Delete(ArchiveModel entity);
        Task Save();
    }
}
