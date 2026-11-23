using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;


namespace FileSageAPI.Entities.Archive.DTOs
{
    public class ArchiveUpdateDTO
    {
        public required int Id { get; set; }
        public AuthorModel? Author { get; set; }
        public BookModel? Book { get; set; }
        public ICollection<UrlInsertNestedDTO>? UrlsInsert { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? UrlsRemove { get; set; }
        public ICollection<int>? TagsLink { get; set; }
        public ICollection<int>? TagsUnlink { get; set; }
        public bool? IsActive { get; set; }

    }
}
