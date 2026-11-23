using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class AuthorMap : IEntityTypeConfiguration<AuthorModel>
    {

        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {

            builder.ToTable("author");

            builder.HasKey(a => a.ArchiveId);

            builder.HasOne(a => a.Archive).WithOne(ci => ci.Author).HasForeignKey<AuthorModel>(a => a.ArchiveId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Books).WithMany(b => b.Authors).UsingEntity<BookAuthorModel>(
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
