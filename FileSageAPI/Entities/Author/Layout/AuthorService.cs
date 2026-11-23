using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Entities.Author.Interfaces;
using FileSageAPI.Entities.Book.Interfaces;

namespace FileSageAPI.Entities.Author.Layout
{
    public class AuthorService : IAuthorService
    {
        private readonly IArchiveService archiveService;

        public AuthorService(IArchiveService _archiveService)
        {
            this.archiveService = _archiveService;
        }

        Task<List<ArchiveModel>> IAuthorService.InsertAuthor(List<AuthorInsertDTO> dtos, bool validate, bool save)
        {
            throw new NotImplementedException();
        }

        Task<List<ArchiveModel>> IAuthorService.UpdateAuthor(List<AuthorUpdateDTO> dtos, bool validate, bool save)
        {
            throw new NotImplementedException();
        }
    }
}
