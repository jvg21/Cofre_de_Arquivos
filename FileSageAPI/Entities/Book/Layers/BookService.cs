using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.DTOs;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Archive.Layers;
using FileSageAPI.Entities.Book.DTOs;
using FileSageAPI.Entities.Book.Interfaces;
using FileSageAPI.Types.Exceptions;
using Mapster;

namespace FileSageAPI.Entities.Book.Layers
{
    public class BookService : IBookService
    {
        private readonly IArchiveService _archiveService;

        public BookService(IArchiveService archiveService)
        {
            this._archiveService = archiveService;
        }
        public async Task<List<ArchiveModel>> Get()
        {
            return await _archiveService.Get(e => e.Book != null);
        }

        public async Task<ArchiveModel> Get(int id)
        {
            return await _archiveService.Get(id, e => e.Book != null);
        }

        public async Task<List<ArchiveModel>> InsertBook(List<BookInsertDTO> dtos, bool validate, bool save)
        {
            var response = new List<ArchiveModel>();

            foreach (var dto in dtos)
            {
                var archive = new ArchiveInsertDTO
                {
                    TagsLink = dto.TagsLink,
                    Urls = dto.Urls,
                    Book = dto.Adapt<BookModel>()
                };

                var request = await _archiveService.Insert(archive, validate, false);
                response.Add(request);
            }

            if (save) await _archiveService.Save();
            return response;
        }

        public async Task<List<ArchiveModel>> UpdateBook(List<BookUpdateDTO> dtos, bool validate, bool save)
        {
            var response = new List<ArchiveModel>();

            foreach (var dto in dtos)
            {
                var book = await _archiveService.Get(dto.Id);
                if (book.Book == null) throw new InvalidFormException($"Archive {dto.Id} not a Book");

                var updatedBook = dto.Adapt(book.Book);

                var archive = new ArchiveUpdateDTO
                {
                    Id = dto.Id,
                    TagsLink = dto.TagsLink,
                    TagsUnlink = dto.TagsUnlink,
                    UrlsInsert = dto.UrlsInsert,
                    UrlsRemove = dto.UrlsRemove,
                    IsActive = dto.IsActive,
                    Book = updatedBook
                };

                var request = await _archiveService.Update(archive, validate, false);
                response.Add(request);
            }


            if (save) await _archiveService.Save();
            return response;
        }


    }
}
