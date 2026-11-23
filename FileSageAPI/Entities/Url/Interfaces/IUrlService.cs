using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Url.Interfaces
{
    public interface IUrlService
    {
        Task<List<UrlModel>> Get();
        Task<UrlModel?> Get(int id);
        Task<List<UrlModel>> Insert(List<UrlInsertDTO> dtos, bool validate, bool save);
        Task<UrlModel> Insert(UrlInsertDTO dto, bool validate, bool save);
        Task<UrlModel> Update(UrlUpdateDTO dto, bool validate, bool save);
        Task<List<UrlModel>> Update(List<UrlUpdateDTO> dtos, bool validate, bool save);
        Task<List<UrlModel>> Delete(List<int> urls, bool validate, bool save);
    }
}
