using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class ArchiveMap: IEntityTypeConfiguration<ArchiveModel>
    {
        public void Configure(EntityTypeBuilder<ArchiveModel> builder)
        {
            builder.ToTable("archive");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.RegisteredAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            builder.HasMany(a => a.Files).WithOne(f => f.Archive).HasForeignKey(a => a.ArchiveId).OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
