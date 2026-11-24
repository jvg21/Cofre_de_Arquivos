using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.DTOs;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Entities.Author.Interfaces;
using FileSageAPI.Entities.Book.Interfaces;
using FileSageAPI.Types.Exceptions;
using Mapster;

namespace FileSageAPI.Entities.Author.Layers
{
    public class AuthorService : IAuthorService
    {
        private readonly IArchiveService _archiveService;

        public AuthorService(IArchiveService archiveService)
        {
            this._archiveService = archiveService;
        }
        public async Task<List<ArchiveModel>> Get()
        {
            return await _archiveService.Get(e => e.Author != null);
        }

        public async Task<ArchiveModel> Get(int id)
        {
            return await _archiveService.Get(id, e => e.Author != null);
        }

        public async Task<List<ArchiveModel>> InsertAuthor(List<AuthorInsertDTO> dtos, bool validate, bool save)
        {
            var response = new List<ArchiveModel>();

            foreach (var dto in dtos)
            {
                var archive = new ArchiveInsertDTO
                {
                    TagsLink = dto.TagsLink,
                    Urls = dto.Urls,
                    Author = new AuthorModel
                    {
                        Name = dto.Name,
                    }
                };

                var request = await _archiveService.Insert(archive, validate, false);
                response.Add(request);
            }

            if (save) await _archiveService.Save();
            return response;
        }
        public async Task<List<ArchiveModel>> UpdateAuthor(List<AuthorUpdateDTO> dtos, bool validate, bool save)
        {
            var response = new List<ArchiveModel>();
            foreach (var dto in dtos)
            {
                var author = await _archiveService.Get(dto.Id);
                if (author.Author == null) throw new InvalidFormException($"Archive {dto.Id} not a Author");

                var archive = new ArchiveUpdateDTO
                {
                    Id = dto.Id,
                    TagsLink = dto.TagsLink,
                    TagsUnlink = dto.TagsUnlink,
                    UrlsInsert = dto.UrlsInsert,
                    UrlsRemove = dto.UrlsRemove,
                    IsActive = dto.IsActive,
                    Author = dto.Adapt(author.Author)
                };
                var request = await _archiveService.Update(archive, validate, false);
                response.Add(request);
            }
            if (save) await _archiveService.Save();
            return response;
        }
    }
}
