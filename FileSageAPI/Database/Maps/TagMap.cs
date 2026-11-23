
using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class TagMap : IEntityTypeConfiguration<TagModel>
    {
        public void Configure(EntityTypeBuilder<TagModel> builder)
        {
            builder.ToTable("tag");
            builder.HasKey((t) => t.Id);

            builder.Property(t => t.Type).HasConversion(v => v.ToLower(), v => v);

            builder.Property(t => t.Content).HasConversion(v => v.ToLower(), v => v);

            builder.HasIndex(t => new { t.Type, t.Content }).IsUnique();

            builder.HasMany(t => t.Archives).WithMany(ci => ci.Tags).UsingEntity<ArchiveTagModel>(
                    j => j.HasOne(cit => cit.Archive).WithMany().HasForeignKey(cit => cit.ArchiveId),
                    j => j.HasOne(cit => cit.Tag).WithMany().HasForeignKey(cit => cit.TagId),
                    j =>
                    {
                        j.HasKey(cit => new { cit.TagId, cit.ArchiveId });
                        j.ToTable("archive_tag");
                    }

                );
        }
    }
}
