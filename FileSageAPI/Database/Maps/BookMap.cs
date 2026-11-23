using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class BookMap:IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.ToTable("book");

            builder.HasKey(b=>b.ArchiveId);

            builder.HasOne(b => b.Archive).WithOne(ci => ci.Book).HasForeignKey<BookModel>(a => a.ArchiveId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.Rating).HasPrecision(3,1);

        }
    }
}
