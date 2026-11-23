using FileSageAPI.Database.AppDbContext;
using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Archive.Layers
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly AppDbContext _context;

        public ArchiveRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<ArchiveModel>> Get()
        {
            return await _context.Archive.Where(e => e.IsActive)
                .Include(e => e.Tags.Where(t => t.IsActive))
                .Include(e => e.Urls)
                .Include(e => e.Author).ThenInclude(a => a!.Books.Where(b => b.Archive.IsActive))
                .Include(e => e.Book).ThenInclude(b => b!.Authors.Where(a => a.Archive.IsActive))
                .AsNoTracking().ToListAsync();
        }

        public async Task<ArchiveModel?> Get(int id)
        {
            return await _context.Archive
                .Include(e => e.Tags.Where(t => t.IsActive))
                .Include(e => e.Urls)
                .Include(e => e.Author).ThenInclude(a => a!.Books.Where(b => b.Archive.IsActive))
                .Include(e => e.Book).ThenInclude(b => b!.Authors.Where(a => a.Archive.IsActive))
                .FirstOrDefaultAsync((e) => e.Id == id && e.IsActive);
        }

        public async Task<List<ArchiveModel>> Get(Expression<Func<ArchiveModel, bool>> predicate)
        {
            return await _context.Archive.Where(e => e.IsActive).Where(predicate)
                .Include(e => e.Tags.Where(t => t.IsActive))
                .Include(e => e.Urls)
                .Include(e => e.Author).ThenInclude(a => a!.Books.Where(b => b.Archive.IsActive))
                .Include(e => e.Book).ThenInclude(b => b!.Authors.Where(a => a.Archive.IsActive))
                .ToListAsync();
        }

        public async Task<ArchiveModel?> Get(int id, Expression<Func<ArchiveModel, bool>> predicate)
        {
            return await _context.Archive.Where(e => e.IsActive).Where(predicate)
                .Include(e => e.Tags.Where(t => t.IsActive))
                .Include(e => e.Urls)
                .Include(e => e.Author).ThenInclude(a => a!.Books.Where(b => b.Archive.IsActive))
                .Include(e => e.Book).ThenInclude(b => b!.Authors.Where(a => a.Archive.IsActive))
                .FirstOrDefaultAsync((e) => e.Id == id);
        }


        public async Task<bool> Exists(Expression<Func<ArchiveModel, bool>> predicate)
        {
            return await _context.Archive.Where(e => e.IsActive).AnyAsync(predicate);
        }
        public async Task<ArchiveModel> Insert(ArchiveModel entity)
        {
            await _context.Archive.AddAsync(entity);
            return entity;
        }
        public async Task<ArchiveModel> Update(ArchiveModel entity)
        {
            _context.Archive.Update(entity);
            _context.Entry(entity).Property(e => e.RegisteredAt).IsModified = false;
            return entity;
        }
        public async Task<ArchiveModel> Delete(ArchiveModel entity)
        {
            var archive = await Get(entity.Id) ?? throw new EntityNotFoundException($"Archive with ID {entity.Id} not found");
            _context.Archive.Remove(archive);
            return archive;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
