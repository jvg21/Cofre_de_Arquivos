
using FileSageAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileSageAPI.Database.Maps
{
    public class UrlMap:IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable("url");

            builder.HasKey(u => u.Id);

            builder.HasOne(u=> u.Archive).WithMany(ci=>ci.Urls).HasForeignKey(u=>u.ArchiveId);
        }
     
    }
}
