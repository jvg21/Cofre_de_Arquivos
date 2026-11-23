using System.Linq.Expressions;
using FileSageAPI.Database.Models;

namespace FileSageAPI.Entities.Tag.Interfaces
{
    public interface ITagRepository
    {
        Task<List<TagModel>> Get();
        Task<TagModel?> Get(int id);
        Task<TagModel?> GetNoTracking(int id);
        Task<bool> Exists(Expression<Func<TagModel, bool>> predicate);
        Task<TagModel> Insert(TagModel entity);
        Task<TagModel> Update(TagModel entity);
        Task<TagModel> Delete(TagModel entity);
        Task Save();
    }
}
