using FileSageAPI.Database.AppDbContext;
using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Tag.Interfaces;
using FileSageAPI.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Tag.Layers
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<TagModel>> Get()
        {
            return await _context.Tag.Where(e => e.IsActive)
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Author)
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Book)
                .AsNoTracking().ToListAsync();
        }

        public async Task<TagModel?> Get(int id)
        {
            return await _context.Tag
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Author)
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Book)
                .FirstOrDefaultAsync((e) => e.Id == id && e.IsActive);
        }

        public async Task<TagModel?> GetNoTracking(int id)
        {
            return await _context.Tag.AsNoTracking()
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Author)
                .Include(e => e.Archives.Where(a => a.IsActive)).ThenInclude(a => a.Book)
                .FirstOrDefaultAsync((e) => e.Id == id && e.IsActive);
        }

        public async Task<bool> Exists(Expression<Func<TagModel, bool>> predicate)
        {
            return await _context.Tag.Where(e => e.IsActive).AnyAsync(predicate);
        }
        public async Task<TagModel> Insert(TagModel entity)
        {
            var exists = await Exists((e) => e.Type == entity.Type && e.Content == entity.Content);
            if (exists) throw new EntityAlreadyExistsException($"Tag type:{entity.Type}, content:{entity.Content} already exists");

            await _context.Tag.AddAsync(entity);
            return entity;
        }
        public async Task<TagModel> Update(TagModel entity)
        {
            var existsId = await Exists((e) => e.Id == entity.Id);
            if (!existsId) throw new EntityNotFoundException($"Tag with ID {entity.Id} not found");

            var exists = await Exists((e) => e.Type == entity.Type && e.Content == entity.Content && e.Id != entity.Id);
            if (exists) throw new EntityAlreadyExistsException($"Tag type:{entity.Type}, content:{entity.Content} already exists");

            _context.Tag.Update(entity);
            return entity;
        }
        public async Task<TagModel> Delete(TagModel entity)
        {
            var url = await Get(entity.Id) ?? throw new EntityNotFoundException($"Tag with ID {entity.Id} not found");

            _context.Tag.Remove(url);
            return url;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
