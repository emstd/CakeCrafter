using AutoMapper;
using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly CakeCrafterDbContext _context;
        private readonly IMapper _mapper;

        public CakeRepository(CakeCrafterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<Cake>> Get(int categoryId, int skip, int take)
        {
            var cakes = await _context.Cakes
                                      .Where(cake => cake.CategoryId == categoryId)
                                      .Skip(skip)
                                      .Take(take)
                                      .Select(cake => _mapper.Map<Cake>(cake))
                                      .ToListAsync();
            return cakes;
        }

        public async Task<Cake?> GetById(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return null;
            }

            var result = _mapper.Map<Cake>(dbCake);

            return result;
        }

        public async Task<int> Create(Cake cake)
        {
            var dbCake = _mapper.Map<CakeEntity>(cake);
            _context.Cakes.Add(dbCake);
            await _context.SaveChangesAsync();

            return dbCake.Id;
        }

        public async Task<Cake?> Update(Cake cake)
        {
            var dbCake = await _context.Cakes.FindAsync(cake.Id);
            if (dbCake == null)
            {
                return null;
            }

            dbCake = _mapper.Map<CakeEntity>(cake);

            _context.Cakes.Update(dbCake);
            await _context.SaveChangesAsync();
            return cake;
        }

        public async Task<bool> Delete(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return false;
            }
            _context.Cakes.Remove(dbCake);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
