using FileSageAPI.Entities.Tag.DTOs;
using FileSageAPI.Entities.Url.DTOs;

namespace FileSageAPI.Entities.Author.DTOs
{
    public class ArchiveAuthorGetDTO
    {
        public int Id { get; set; }
        public AuthorGetDTO? Author { get; set; }
        public ICollection<UrlGetSummaryDTO> Urls { get; set; } = new List<UrlGetSummaryDTO>();
        public ICollection<TagGetDTO> Tags { get; set; } = new List<TagGetDTO>();

        public DateTime RegisteredAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
