using FileSageAPI.Database.Models;

namespace FileSageAPI.Entities.Url.DTOs
{
    public class UrlUpdateDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }

        public int? ArchiveId { get; set; }
    }
}
