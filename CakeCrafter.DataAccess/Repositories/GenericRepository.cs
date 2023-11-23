using CakeCrafter.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private CakeCrafterDbContext _context;
        private DbSet<TModel> _table;

        public GenericRepository(CakeCrafterDbContext context)
        {
            _context = context;
            _table = _context.Set<TModel>();
        }

        public async Task<TModel> Create(TModel model)
        {
            _table.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return false;
            }
            _table.Remove(dbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TModel>> Get()
        {
            return await _table.ToListAsync();
        }

        public async Task<TModel?> GetById(int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return null;
            }
            return dbModel;
        }

        public async Task<TModel?> Update(TModel model, int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return null;
            }
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
