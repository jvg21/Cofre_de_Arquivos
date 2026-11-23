using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;


namespace FileSageAPI.Entities.Author.DTOs
{
    public class AuthorInsertDTO
    {
        public required string Name { get; set; }
        //public ICollection<BookInsertNestedDTO> BooksInsert { get; set; } = new List<BookInsertNestedDTO>();
        //public ICollection<int>? BooksLink { get; set; } = new List<int>();
        public ICollection<UrlInsertNestedDTO>? Urls { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? TagsLink { get; set; } = new List<int>();

    }
}
