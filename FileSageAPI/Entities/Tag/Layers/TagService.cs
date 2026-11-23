using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Tag.DTOs;
using FileSageAPI.Entities.Tag.Interfaces;
using FileSageAPI.Types.Exceptions;
using Mapster;

namespace FileSageAPI.Entities.Tag.Layers
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;

        }

        public async Task<List<TagModel>> Get()
        {
            var request = await _repository.Get();
            return request;
        }
        public async Task<TagModel> Get(int id)
        {
            var request = await _repository.Get(id) ?? throw new EntityNotFoundException($"Tag {id} not found");
            return request;
        }

        public async Task<TagModel> GetNoTracking(int id)
        {
            var request = await _repository.GetNoTracking(id) ?? throw new EntityNotFoundException($"Tag {id} not found");
            return request;
        }

        public async Task<TagModel> Insert(TagInsertDTO dto, bool validate, bool save)
        {
            var tag = dto.Adapt<TagModel>();

            if (validate) tag.IsValidInsert();
            var request = await _repository.Insert(tag);

            if (save) await _repository.Save();
            return request;
        }
        public async Task<List<TagModel>> Insert(List<TagInsertDTO> dtos, bool validate, bool save)
        {
            var response = new List<TagModel>();
            foreach (var dto in dtos)
            {
                var tag = await Insert(dto, validate, false);
                response.Add(tag);
            }
            if (save) await _repository.Save();

            return response;
        }


        public async Task<TagModel> Update(TagUpdateDTO dto, bool validate, bool save)
        {
            var tag = await _repository.Get(dto.Id) ?? throw new EntityNotFoundException($"Tag {dto.Id} not found");

            // if(dto.Content != null) tag.Content = dto.Content;
            // if(dto.Type != null) tag.Type = dto.Type;
            // if(dto.IsActive != null) tag.IsActive = dto.IsActive.Value;

            //adapt the values from the dto to the tag, ignoring null values
            dto.Adapt(tag);

            if (validate) tag.IsValidInsert();
            var request = await _repository.Update(tag);

            if (save) await _repository.Save();
            return request;
        }
        public async Task<List<TagModel>> Update(List<TagUpdateDTO> dtos, bool validate, bool save)
        {
            var unique = dtos.DistinctBy(x => x.Id).ToList();
            if (unique.Count != dtos.Count) throw new InvalidFormException("Duplicate tags found in the request");

            var response = new List<TagModel>();
            foreach (var dto in dtos)
            {
                var tag = await Update(dto, validate, false);
                response.Add(tag);
            }
            if (save) await _repository.Save();

            return response;
        }


        
    }
}
