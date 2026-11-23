using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Url.Interfaces
{
    public interface IUrlRepository
    {
        Task<List<UrlModel>> Get();
        Task<UrlModel?> Get(int id);
        Task<bool> Exists(Expression<Func<UrlModel, bool>> predicate);
        Task<UrlModel> Insert(UrlModel entity);
        Task<UrlModel> Update(UrlModel entity);
        Task<UrlModel> Delete(UrlModel entity);
        void ChangeState(UrlModel entity, EntityState state);
        Task Save();
    }
}
