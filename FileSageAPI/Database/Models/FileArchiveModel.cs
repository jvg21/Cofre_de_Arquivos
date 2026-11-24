using FileSageAPI.Entities.Author.DTOs;

namespace FileSageAPI.Database.Models
{
    public class FileArchiveModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? StorageName { get; set; }
        public string? Extension { get; set; }
        public string? MimeType { get; set; }
        public long? StorageBytes { get; set; }
        public string? Path { get; set; }

        public int ArchiveId { get; set; }
        public ArchiveModel? Archive { get; set; }

    }
}
