using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class FileArchiveMap:IEntityTypeConfiguration<FileArchiveModel>
    {

        public void Configure(EntityTypeBuilder<FileArchiveModel> builder)
        {
            builder.ToTable("file_archive");
            builder.HasKey((t) => t.Id);

        }
    }
}
