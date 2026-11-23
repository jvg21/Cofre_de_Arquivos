using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Book.DTOs;
using FileSageAPI.Entities.Book.Interfaces;

namespace FileSageAPI.Entities.Book.Layout
{
    public class BookService : IBookService
    {
        private readonly IArchiveService archiveService;

        public BookService(IArchiveService _archiveService)
        {
            this.archiveService = _archiveService;
        }

        Task<List<ArchiveModel>> IBookService.InsertBook(List<BookInsertDTO> dtos, bool validate, bool save)
        {
            throw new NotImplementedException();
        }

        Task<List<ArchiveModel>> IBookService.UpdateBook(List<BookUpdateDTO> dtos, bool validate, bool save)
        {
            throw new NotImplementedException();
        }
    }
}
