using FileSageAPI.Database.Models;

namespace FileSageAPI.Entities.Url.DTOs
{
    public class UrlGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public int ArchiveId { get; set; }
    }
}
