
using FileSageAPI.Database.Models;
using FileSageAPI.Database.Maps;
using Microsoft.EntityFrameworkCore;
namespace FileSageAPI.Database.AppDbContext
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Archive> Archive { get; set; }
        public DbSet<Url> Url { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<ArchiveTag> ArchiveTag { get; set; }

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
