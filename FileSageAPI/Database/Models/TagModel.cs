using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ICollection<ArchiveModel> Archives { get; set; } = new List<ArchiveModel>();
        public bool IsActive { get; set; } = true;


      public string IsValidInsert(bool ThrowException = true)
        {
            if (string.IsNullOrWhiteSpace(this.Content))
            {
                var message = "Content Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            if (string.IsNullOrWhiteSpace(this.Type))
            {
                var message = "Tag Type Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            this.Content = this.Content.Trim().ToLower();
            this.Type = this.Type.Trim().ToLower();

            return "Tag is valid";
        }

    }
}
