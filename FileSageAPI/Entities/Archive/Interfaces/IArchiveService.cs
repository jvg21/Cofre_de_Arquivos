using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.DTOs;
using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Entities.Book.DTOs;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Archive.Interfaces
{
    public interface IArchiveService
    {
        Task<List<ArchiveModel>> Get();
        Task<List<ArchiveModel>> Get(Expression<Func<ArchiveModel, bool>> predicate);
        Task<ArchiveModel> Get(int id);
        Task<ArchiveModel> Get(int id, Expression<Func<ArchiveModel, bool>> predicate);
        Task<bool> Exists(Expression<Func<ArchiveModel, bool>> predicate);

        Task<ArchiveModel> Insert(ArchiveInsertDTO dto, bool validate, bool save);
        Task<List<ArchiveModel>> Insert(List<ArchiveInsertDTO> dtos, bool validate, bool save);
        Task<ArchiveModel> Update(ArchiveUpdateDTO dto, bool validate, bool save);
        Task<List<ArchiveModel>> Update(List<ArchiveUpdateDTO> dtos, bool validate, bool save);


        //Task<List<ArchiveModel>> Delete(List<int> archives, bool validate, bool save);
    }
}
