using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class BookAuthorModel
    {
        public int BookId { get; set; }
        public BookModel? Book { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel? Author { get; set; }

        public string IsValidInsert(bool ThrowException = true)
        {
            if (BookId < 0 || AuthorId < 0)

            {
                var message = "Id Invalid";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            return "BookAuthor is valid";
        }
    }
}
