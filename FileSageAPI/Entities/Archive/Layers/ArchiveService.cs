using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.DTOs;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Tag.Interfaces;
using FileSageAPI.Entities.Url;
using FileSageAPI.Types.Exceptions;

using Mapster;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Archive.Layers
{
    public class ArchiveService : IArchiveService
    {
        private readonly IArchiveRepository _repository;
        //private readonly IArchiveTagRepository _archiveTagRepository;
        private readonly ITagRepository _tagRepository;

        public ArchiveService(IArchiveRepository repository,  ITagRepository tagRepository)
        {
            _repository = repository;
            //_archiveTagRepository = archiveTagRepository;
            _tagRepository = tagRepository;
        }

        public async Task<List<ArchiveModel>> Get()
        {
            var request = await _repository.Get();
            return request;
        }
        public async Task<ArchiveModel> Get(int id)
        {
            var request = await _repository.Get(id) ?? throw new EntityNotFoundException($"Archive ID {id} not found");
            return request;
        }

        public async Task<List<ArchiveModel>> Get(Expression<Func<ArchiveModel, bool>> predicate)
        {
            var request = await _repository.Get(predicate);
            return request;
        }

        public async Task<ArchiveModel> Get(int id, Expression<Func<ArchiveModel, bool>> predicate)
        {
            var request = await _repository.Get(id, predicate) ?? throw new EntityNotFoundException($"Archive not found");
            return request;
        }

        public async Task<bool> Exists(Expression<Func<ArchiveModel, bool>> predicate)
        {
            return await _repository.Exists(predicate);
        }

        public async Task<ArchiveModel> Insert(ArchiveInsertDTO dto, bool validate, bool save)
        {
            var tags = new List<TagModel>();
            if (dto.TagsLink != null)
            {
                foreach (var tagId in dto.TagsLink)
                {
                    var tag = await _tagRepository.Get(tagId) ?? throw new EntityNotFoundException($"Tag {tagId} not found");
                    if (!tags.Any(t => t.Id == tag.Id)) tags.Add(tag);
                }
            }

            var urls = new List<UrlModel>();

            if (dto.Urls != null)
            {
                foreach (var url in dto.Urls)
                {
                    if (string.IsNullOrWhiteSpace(url.Name)) url.Name = UrlFunctions.GenerateUrlName(url.Content) ?? url.Content;
                    urls.Add(url.Adapt<UrlModel>());
                }
            }

            var archive = new ArchiveModel
            {
                Author = dto.Author,
                Book = dto.Book,
                Tags = tags,
                Urls = urls,
                RegisteredAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (validate) archive.IsValidInsert();

            var request = await _repository.Insert(archive);

            if (save) await _repository.Save();

            return request;
        }

        public async Task<List<ArchiveModel>> Insert(List<ArchiveInsertDTO> dtos, bool validate, bool save)
        {
            var response = new List<ArchiveModel>();

            foreach (var dto in dtos)
            {
                var archive = await Insert(dto, validate, false);
                response.Add(archive);
            }

            if (save) await _repository.Save();
            return response;
        }

        //public async Task<List<Archive>> InsertAuthor(List<AuthorInsertDTO> dtos, bool validate, bool save)
        //{
        //    var response = new List<Archive>();

        //    foreach (var dto in dtos)
        //    {

        //        var archive = new ArchiveInsertDTO
        //        {
        //            TagsLink = dto.TagsLink,
        //            Urls = dto.Urls,
        //            Author = new Author
        //            {
        //                Name = dto.Name,
        //            }
        //        };

        //        var request = await Insert(archive, validate, false);
        //        response.Add(request);
        //    }

        //    if (save) await _repository.Save();
        //    return response;
        //}

        //public async Task<List<Archive>> InsertBook(List<BookInsertDTO> dtos, bool validate, bool save)
        //{
        //    var response = new List<Archive>();

        //    foreach (var dto in dtos)
        //    {

        //        var archive = new ArchiveInsertDTO
        //        {
        //            TagsLink = dto.TagsLink,
        //            Urls = dto.Urls,
        //            Book = dto.Adapt<Book>()
        //        };

        //        var request = await Insert(archive, validate, false);
        //        response.Add(request);
        //    }

        //    if (save) await _repository.Save();
        //    return response;
        //}


        public async Task<ArchiveModel> Update(ArchiveUpdateDTO dto, bool validate, bool save)
        {
            var archive = await _repository.Get(dto.Id) ?? throw new EntityNotFoundException($"Archive {dto.Id} not found");

            var tags = archive.Tags.ToList();
            if (dto.TagsLink != null)
            {
                foreach (var tagId in dto.TagsLink)
                {
                    var tag = await _tagRepository.Get(tagId) ?? throw new EntityNotFoundException($"Tag {tagId} not found");
                    if (!tags.Any(t => t.Id == tag.Id)) tags.Add(tag);
                }
            }

            if (dto.TagsUnlink != null)
            {
                tags.RemoveAll(tag => dto.TagsUnlink.Contains(tag.Id));
            }

            var urls = archive.Urls.ToList();

            if (dto.UrlsInsert != null)
            {
                foreach (var url in dto.UrlsInsert)
                {
                    if (string.IsNullOrWhiteSpace(url.Name)) url.Name = UrlFunctions.GenerateUrlName(url.Content) ?? url.Content;
                    urls.Add(url.Adapt<UrlModel>());
                }
            }

            if (dto.UrlsRemove != null)
            {
                urls.RemoveAll(url => dto.UrlsRemove.Contains(url.Id));
            }

            if (dto.Author != null) archive.Author = dto.Author;
            if (dto.Book != null) archive.Book = dto.Book;

            archive.Tags = tags;
            archive.Urls = urls;
            archive.UpdatedAt = DateTime.UtcNow;
            if (dto.IsActive != null) archive.IsActive = dto.IsActive.Value;

            if (validate) archive.IsValidInsert();

            var request = await _repository.Update(archive);

            if (save) await _repository.Save();

            return request;
        }

        public async Task<List<ArchiveModel>> Update(List<ArchiveUpdateDTO> dtos, bool validate, bool save)
        {
            var unique = dtos.DistinctBy(t => t.Id).ToList();
            if (unique.Count != dtos.Count) throw new InvalidFormException("Duplicate archives found in the request");
            var response = new List<ArchiveModel>();

            foreach (var dto in dtos)
            {
                var archive = await Update(dto, validate, false);
                response.Add(archive);
            }

            if (save) await _repository.Save();
            return response;
        }

        //public async Task<List<Archive>> UpdateBook(List<BookUpdateDTO> dtos, bool validate, bool save)
        //{
        //    var response = new List<Archive>();

        //    foreach (var dto in dtos)
        //    {
        //        var book = await _repository.Get(dto.Id) ?? throw new EntityNotFoundException($"Archive {dto.Id} not found");
        //        if (book.Book == null) throw new InvalidFormException($"Archive {dto.Id} not a Book");

        //        var updatedBook = dto.Adapt(book.Book);

        //        var archive = new ArchiveUpdateDTO
        //        {
        //            Id = dto.Id,
        //            TagsLink = dto.TagsLink,
        //            TagsUnlink = dto.TagsUnlink,
        //            UrlsInsert = dto.UrlsInsert,
        //            UrlsRemove = dto.UrlsRemove,
        //            IsActive = dto.IsActive,
        //            Book = updatedBook
        //        };

        //        var request = await Update(archive, validate, false);
        //        response.Add(request);
        //    }


        //    if (save) await _repository.Save();
        //    return response;
        //}

        //public async Task<List<Archive>> UpdateAuthor(List<AuthorUpdateDTO> dtos, bool validate, bool save)
        //{
        //    var response = new List<Archive>();
        //    foreach (var dto in dtos)
        //    {
        //        var author = await _repository.Get(dto.Id) ?? throw new EntityNotFoundException($"Archive {dto.Id} not found");
        //        if (author.Author == null) throw new InvalidFormException($"Archive {dto.Id} not a Author");

        //        var updatedAuthor = dto.Adapt(author.Author);

        //        var archive = new ArchiveUpdateDTO
        //        {
        //            Id = dto.Id,
        //            TagsLink = dto.TagsLink,
        //            TagsUnlink = dto.TagsUnlink,
        //            UrlsInsert = dto.UrlsInsert,
        //            UrlsRemove = dto.UrlsRemove,
        //            IsActive = dto.IsActive,
        //            Author = updatedAuthor
        //        };
        //        var request = await Update(archive, validate, false);
        //        response.Add(request);
        //    }
        //    if (save) await _repository.Save();
        //    return response;
        //}
        //public async Task<List<Archive>> Delete(List<int> archives, bool validate, bool save)
        //{
        //    var uniqueIds = archives.Distinct().ToList();
        //    if (uniqueIds.Count != archives.Count) throw new InvalidFormException("Duplicate archives found in the request");

        //    var response = new List<Archive>();

        //    foreach (var urlId in uniqueIds)
        //    {
        //        //if(validate) validar se existe uma ligação com outras tabelas
        //        var request = await _repository.Delete(new Archive { Id = urlId });
        //        response.Add(request);
        //    }
        //    if (save) await _repository.Save();

        //    return response;
        //}

    }
}
