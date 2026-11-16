using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class Author
    {
        public int ArchiveId { get; set; }
        public Archive Archive { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public string IsValidInsert(bool ThrowException = true)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                var message = "Name Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            return "Author is valid";
        }

    }

}
