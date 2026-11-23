using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;


namespace FileSageAPI.Entities.Archive.DTOs
{
    public class ArchiveInsertDTO
    {
        public AuthorModel? Author { get; set; }
        public BookModel? Book { get; set; }
        public ICollection<UrlInsertNestedDTO>? Urls { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? TagsLink { get; set; } = new List<int>();

    }
}
