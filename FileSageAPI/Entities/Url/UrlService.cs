using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;
using FileSageAPI.Entities.Url.Interfaces;
using FileSageAPI.Types.Exceptions;

using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FileSageAPI.Entities.Url
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _repository;

        public UrlService(IUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UrlModel>> Get()
        {
            var request = await _repository.Get();
            return request;
        }

        public async Task<UrlModel?> Get(int id)
        {
            var request = await _repository.Get(id) ?? throw new EntityNotFoundException($"url ID {id} not found");
            return request;
        }

        public async Task<UrlModel> Insert(UrlInsertDTO dto, bool validate, bool save)
        {
            var url = dto.Adapt<UrlModel>();
            if (string.IsNullOrWhiteSpace(url.Name)) url.Name = UrlFunctions.GenerateUrlName(url.Content) ?? url.Content;

            if (validate) url.IsValidInsert();

            var request = await _repository.Insert(url);

            if (save) await _repository.Save();
            return request;
        }


        public async Task<List<UrlModel>> Insert(List<UrlInsertDTO> dtos, bool validate, bool save)
        {
            var response = new List<UrlModel>();
            foreach (var dto in dtos)
            {
                var url = await Insert(dto, validate, false);
                response.Add(url);
            }
            if (save) await _repository.Save();

            return response;
        }


        public async Task<UrlModel> Update(UrlUpdateDTO dto, bool validate, bool save)
        {

            var url = await _repository.Get(dto.Id) ?? throw new EntityNotFoundException($"Url {dto.Id} not found");

            dto.Adapt(url);
            if (string.IsNullOrWhiteSpace(url.Name)) url.Name = UrlFunctions.GenerateUrlName(url.Content) ?? url.Content;

            if (validate) url.IsValidInsert();

            var request = await _repository.Update(url);

            if (save) await _repository.Save();
            return request;
        }


        public async Task<List<UrlModel>> Update(List<UrlUpdateDTO> dtos, bool validate, bool save)
        {
            var uniqueIds = dtos.DistinctBy(e => e.Id).ToList();
            if (uniqueIds.Count != dtos.Count) throw new InvalidFormException("Duplicate url found in the request");

            var response = new List<UrlModel>();
            foreach (var dto in dtos)
            {
                var url = await Update(dto, validate, false);
                response.Add(url);
            }
            if (save) await _repository.Save();

            return response;
        }

        public async Task<List<UrlModel>> Delete(List<int> urls, bool validate, bool save)
        {
            var uniqueIds = urls.Distinct().ToList();
            if (uniqueIds.Count != urls.Count) throw new InvalidFormException("Duplicate url found in the request");

            var response = new List<UrlModel>();

            foreach (var urlId in uniqueIds)
            {
                var request = await _repository.Delete(new UrlModel { Id = urlId });

                response.Add(request);
            }
            if (save) await _repository.Save();

            return response;
        }


    }
}
