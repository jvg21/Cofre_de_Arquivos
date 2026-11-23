using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.DTOs;
using FileSageAPI.Types.Enums;

namespace FileSageAPI.Entities.Book.DTOs
{
    public class BookInsertDTO
    {
        public required string Title { get; set; }
        public required string Summary { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public double? Rating { get; set; }
        public int? TotalChapters { get; set; }
        public int? CurrentChapter { get; set; }
        public int? Words { get; set; }
        public ReadingStatus? ReadingStatus { get; set; }
        public WritingStatus? WritingStatus { get; set; }
        public DateOnly? Published { get; set; }
        public DateOnly? LastUpdate { get; set; }
        public ICollection<UrlInsertNestedDTO>? Urls { get; set; } = new List<UrlInsertNestedDTO>();
        public ICollection<int>? TagsLink { get; set; } = new List<int>();
        //public ICollection<int>? AuthorsLink { get; set; } = new List<int>();
    }
}
