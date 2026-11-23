using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Author.DTOs;

namespace FileSageAPI.Entities.Author.Interfaces
{
    public interface IAuthorService
    {

        Task<List<ArchiveModel>> InsertAuthor(List<AuthorInsertDTO> dtos, bool validate, bool save);
        Task<List<ArchiveModel>> UpdateAuthor(List<AuthorUpdateDTO> dtos, bool validate, bool save);
    }
}
