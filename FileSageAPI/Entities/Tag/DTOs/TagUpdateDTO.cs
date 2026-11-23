namespace FileSageAPI.Entities.Tag.DTOs
{
    public class TagUpdateDTO
    {
        public required int Id { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        public bool? IsActive { get; set; }
    }
}
