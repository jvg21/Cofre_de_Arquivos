using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;


namespace FileSageAPI.Entities.Author.DTOs
{
    public class AuthorInsertDTO
    {
        public required string Name { get; set; }
        public ICollection<UrlInsertNestedDTO>? Urls { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? TagsLink { get; set; } = new List<int>();

    }
}
