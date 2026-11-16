using FileSageAPI.Types.Enums;
using FileSageAPI.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FileSageAPI.Database.Models
{
    public class Book
    {
        public int ArchiveId { get; set; }
        public Archive Archive { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public double? Rating { get; set; }
        public int? Words { get; set; }
        public int? TotalChapters { get; set; }
        public int? CurrentChapter { get; set; } = 0;
        public ReadingStatus? ReadingStatus { get; set; }
        public WritingStatus? WritingStatus { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public DateOnly? Published { get; set; }
        public DateOnly? LastUpdate { get; set; }


        public string IsValidInsert(bool ThrowException = true)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                var message = "Title Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (string.IsNullOrWhiteSpace(Summary))
            {
                var message = "Summary Invalid, mustn't be empty";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (this.Rating.HasValue && (this.Rating > 10 || this.Rating < 0))
            {
                var message = "Rating Invalid, must be between 0 and 10";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (Words.HasValue && Words.Value < 0)
            {
                var message = "Words Invalid, must be greater than or equal to 0";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (CurrentChapter.HasValue && CurrentChapter.Value < 0)
            {
                var message = "Current Chapter Invalid, must be greater than or equal to 0";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (TotalChapters.HasValue && TotalChapters.Value < 0)
            {
                var message = "Total Chapters Invalid, must be greater than or equal to 0";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (TotalChapters.HasValue && CurrentChapter.HasValue && TotalChapters.Value < CurrentChapter.Value)
            {
                var message = "Current Chapter value is Higher than Total Chapters";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (ReadingStatus.HasValue && !Enum.IsDefined(typeof(ReadingStatus), ReadingStatus.Value))
            {
                var message = "Invalid reading status";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (WritingStatus.HasValue && !Enum.IsDefined(typeof(WritingStatus), WritingStatus.Value))
            {
                var message = "Invalid writing status";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (Published.HasValue && Published.Value > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                var message = "Published date cannot be in the future";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (LastUpdate.HasValue && LastUpdate.Value > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                var message = "Last Update date cannot be in the future";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            if (Published.HasValue && LastUpdate.HasValue && Published.Value > LastUpdate.Value)
            {
                var message = "Published date cannot be later than Last Update";
                if (ThrowException) throw new InvalidFormException(message);
                return message;
            }

            return "Book is valid";
        }

    }
}
