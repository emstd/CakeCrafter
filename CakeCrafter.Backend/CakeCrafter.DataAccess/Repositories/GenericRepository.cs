using AutoMapper;
using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class GenericRepository<TModel, TEntity> : IGenericRepository<TModel> where TModel : class where TEntity : class, IEntity
    {
        private readonly CakeCrafterDbContext _context;
        private readonly DbSet<TEntity> _table;
        private readonly IMapper _mapper;

        public GenericRepository(CakeCrafterDbContext context, IMapper mapper)
        {
            _context = context;
            _table = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<List<TModel>> Get()
        {
            var dbModels = await _table.ToListAsync();
            var models = new List<TModel>();

            foreach (var dbModel in dbModels)
            {
                models.Add(_mapper.Map<TModel>(dbModel));
            }

            return models;
        }

        public async Task<TModel?> GetById(int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            var result = _mapper.Map<TModel>(dbModel);


            return result;
        }

        public async Task<TModel> Create(TModel model)
        {
            var dbModel = _mapper.Map<TEntity>(model);
            _table.Add(dbModel);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TModel?> Update(TModel model, int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            dbModel = _mapper.Map<TEntity>(model);

            _context.Update(dbModel);
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
    }
}
