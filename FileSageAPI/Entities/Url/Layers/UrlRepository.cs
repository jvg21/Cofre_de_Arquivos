using FileSageAPI.Database.AppDbContext;
using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Url.Interfaces;
using FileSageAPI.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSageAPI.Entities.Url.Layers
{
    public class UrlRepository:IUrlRepository
    {
        private readonly AppDbContext _context;

        public UrlRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<UrlModel>> Get()
        {
            return await _context.Url.AsNoTracking().ToListAsync();
        }

        public async Task<UrlModel?> Get(int id)
        {
            return await _context.Url.FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<bool> Exists(Expression<Func<UrlModel, bool>> predicate)
        {
            return await _context.Url.AnyAsync(predicate);
        }
        public async Task<UrlModel> Insert(UrlModel entity)
        {
            await _context.Url.AddAsync(entity);
            return entity;
        }
        public async Task<UrlModel> Update(UrlModel entity)
        {
            var valid = await Exists(e => e.Id == entity.Id);
            if (!valid) throw new EntityNotFoundException($"Url with ID {entity.ArchiveId} not found");

            _context.Url.Update(entity);
            return entity;
        }
        public async Task<UrlModel> Delete(UrlModel entity)
        {
            var url = await Get(entity.Id)??throw new EntityNotFoundException($"Url with ID {entity.Id} not found");

            _context.Url.Remove(url);
            return url;
        }

        public async void ChangeState(UrlModel entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
