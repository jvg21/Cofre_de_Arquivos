using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class ArchiveMap: IEntityTypeConfiguration<Archive>
    {
        public void Configure(EntityTypeBuilder<Archive> builder)
        {
            builder.ToTable("archive");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.RegisteredAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        }
    }
}
