using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class UrlModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public int ArchiveId { get; set; }
        public ArchiveModel? Archive { get; set; }


        public string IsValidInsert(bool ThrowException = true)
        {
            if (string.IsNullOrWhiteSpace(this.Content))
            {
                var message = "Content Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                var message = "Url Name Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            return "Url is valid";
        }
    }
}
