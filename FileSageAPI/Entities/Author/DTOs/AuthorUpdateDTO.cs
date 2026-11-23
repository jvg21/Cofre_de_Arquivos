using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;


namespace FileSageAPI.Entities.Author.DTOs
{
    public class AuthorUpdateDTO
    {
        public required int Id { get; set; }
        public  string? Name { get; set; }

        public ICollection<UrlInsertNestedDTO>? UrlsInsert { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? UrlsRemove { get; set; }
        public ICollection<int>? TagsLink { get; set; }
        public ICollection<int>? TagsUnlink { get; set; }
        //public ICollection<BookInsertNestedDTO> BooksInsert { get; set; } = new List<BookInsertNestedDTO>();
        //public ICollection<int>? BooksLink { get; set; }
        //public ICollection<int>? BooksUnlink { get; set; }
        public bool? IsActive { get; set; }
    }
}
