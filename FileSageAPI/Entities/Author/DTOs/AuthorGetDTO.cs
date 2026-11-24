using FileSageAPI.Entities.Book.DTOs;

namespace FileSageAPI.Entities.Author.DTOs
{
    public class AuthorGetDTO
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<BookGetSummaryDTO> Books { get; set; } = new List<BookGetSummaryDTO>();
    }
}
