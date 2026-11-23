using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Tag.DTOs;

namespace FileSageAPI.Entities.Tag.Interfaces
{
    public interface ITagService
    {
        Task<List<TagModel>> Get();
        Task<TagModel> Get(int id);
        Task<TagModel> GetNoTracking(int id);
        Task<TagModel> Insert(TagInsertDTO dto, bool validate, bool save);
        Task<List<TagModel>> Insert(List<TagInsertDTO> dtos, bool validate, bool save);
        Task<TagModel> Update(TagUpdateDTO dto, bool validate, bool save);
        Task<List<TagModel>> Update(List<TagUpdateDTO> dtos, bool validate, bool save);
    }
}