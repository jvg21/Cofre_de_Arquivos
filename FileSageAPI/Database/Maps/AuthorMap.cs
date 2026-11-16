using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {

        public void Configure(EntityTypeBuilder<Author> builder)
        {

            builder.ToTable("author");

            builder.HasKey(a => a.ArchiveId);

            builder.HasOne(a => a.Archive).WithOne(ci => ci.Author).HasForeignKey<Author>(a => a.ArchiveId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Books).WithMany(b => b.Authors).UsingEntity<BookAuthor>(
                    j => j.HasOne(ba => ba.Book).WithMany().HasForeignKey(ba=>ba.BookId),
                    j => j.HasOne(ba => ba.Author).WithMany().HasForeignKey(ba=>ba.AuthorId),
                    j =>
                    {
                        j.HasKey(ba => new { ba.AuthorId, ba.BookId });
                        j.ToTable("book_author");
                    }
                );

        }
    }
}
