
using FileSageAPI.Database.Models;
using FileSageAPI.Database.Maps;
using Microsoft.EntityFrameworkCore;
namespace FileSageAPI.Database.AppDbContext
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ArchiveModel> Archive { get; set; }
        public DbSet<UrlModel> Url { get; set; }
        public DbSet<TagModel> Tag { get; set; }

        public DbSet<AuthorModel> Author { get; set; }
        public DbSet<BookModel> Book { get; set; }
        public DbSet<BookAuthorModel> BookAuthor { get; set; }
        public DbSet<ArchiveTagModel> ArchiveTag { get; set; }

        //public DbSet<FileArchive> FileArchive { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArchiveMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new UrlMap());
            modelBuilder.ApplyConfiguration(new TagMap());
            //modelBuilder.ApplyConfiguration(new FileArchiveMap());
        }
    }
}
