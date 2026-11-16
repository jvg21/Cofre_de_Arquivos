using FileSageAPI.Types.Enums;
using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class Archive
    {
        public int Id { get; set; }

        //public CatalogItemTypes Type { get; set; }

        public ICollection<Url> Urls { get; set; } = new List<Url>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public Author? Author { get; set; }
        public Book? Book { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


        public string IsValidInsert(bool ThrowException = true)
        {
            var count = 0;

            if (Urls != null && Urls.Count > 0)
            {
                foreach (var url in Urls)
                {
                    var result = url.IsValidInsert(ThrowException);
                    if (result != "Url is valid") return result;
                }
            }

            if (Author != null)
            {
                count++;
                var result = Author.IsValidInsert(ThrowException);
                if (result != "Author is valid") return result;
            }

            if (Book != null)
            {
                count++;
                var result = Book.IsValidInsert(ThrowException);
                if (result != "Book is valid") return result;
            }

            if (count > 1)
            {
                var message = "Archive can only have one of the following: Author, Book";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            return "Archive is valid";
        }
    }
}
