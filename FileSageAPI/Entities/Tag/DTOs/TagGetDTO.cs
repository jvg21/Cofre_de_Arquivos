

namespace FileSageAPI.Entities.Tag.DTOs;

public class TagGetDTO
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    //public ICollection<ArchiveGetSummaryDTO> Archives { get; set; } = new List<ArchiveGetSummaryDTO>();
    public bool IsActive { get; set; }
}
