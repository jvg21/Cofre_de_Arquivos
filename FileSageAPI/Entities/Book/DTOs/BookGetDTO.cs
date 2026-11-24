using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Types.Enums;

namespace FileSageAPI.Entities.Book.DTOs
{
    public class BookGetDTO
    {
        public int ArchiveId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public double? Rating { get; set; }
        public int? Words { get; set; }
        public int? TotalChapters { get; set; }
        public int? CurrentChapter { get; set; } = 0;
        public ReadingStatus? ReadingStatus { get; set; }
        public WritingStatus? WritingStatus { get; set; }
        public ICollection<AuthorGetSummaryDTO> Authors { get; set; } = new List<AuthorGetSummaryDTO>();
        public DateOnly? Published { get; set; }
        public DateOnly? LastUpdate { get; set; }
    }
}
