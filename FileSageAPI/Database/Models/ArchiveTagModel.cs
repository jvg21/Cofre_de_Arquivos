using FileSageAPI.Types.Exceptions;

namespace FileSageAPI.Database.Models
{
    public class ArchiveTagModel
    {
        public int ArchiveId { get; set; }
        public ArchiveModel? Archive { get; set; }
        public int TagId { get; set; }
        public TagModel? Tag { get; set; }


        public string IsValidInsert(bool ThrowException = true)
        {
            if (ArchiveId < 0 || TagId < 0)

            {
                var message = "Id Invalid ";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }
            return "ArchiveTag is valid";
        }
    }
}
