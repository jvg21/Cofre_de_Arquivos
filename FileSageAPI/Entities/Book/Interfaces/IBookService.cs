using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Book.DTOs;

namespace FileSageAPI.Entities.Book.Interfaces
{
    public interface IBookService
    {

        Task<List<ArchiveModel>> InsertBook(List<BookInsertDTO> dtos, bool validate, bool save);
        Task<List<ArchiveModel>> UpdateBook(List<BookUpdateDTO> dtos, bool validate, bool save);
    }
}
