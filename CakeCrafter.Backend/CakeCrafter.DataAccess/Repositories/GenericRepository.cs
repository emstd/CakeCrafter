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
            var dbModels = await _table.AsNoTracking().Select(cake => _mapper.Map<TModel>(cake)).ToListAsync();

            return dbModels;
        }

        public async Task<TModel?> GetById(int id)
        {
            var dbModel = await _table.AsNoTracking().FirstOrDefaultAsync(model => model.Id == id);
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
            await _table.AddAsync(dbModel);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TModel?> Update(TModel model, int id)
        {
            var dbModel = await _table.AsNoTracking().FirstOrDefaultAsync(dbmodel => dbmodel.Id == id);
            if (dbModel == null)
            {
                return null;
            }

            dbModel = _mapper.Map<TEntity>(model);

            _context.Update(dbModel);               //Имеет ли смысл использовать ExecuteUpdate()?
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
            await _table.Where(model => model.Id == id).ExecuteDeleteAsync();
            return true;
        }
    }
}
