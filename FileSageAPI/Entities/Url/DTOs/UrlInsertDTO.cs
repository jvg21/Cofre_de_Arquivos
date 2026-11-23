namespace FileSageAPI.Entities.Url.DTOs
{
    public class UrlInsertDTO
    {
        public required string Name { get; set; }
        public required string Content { get; set; }

        public required int ArchiveId { get; set; }
    }
}
