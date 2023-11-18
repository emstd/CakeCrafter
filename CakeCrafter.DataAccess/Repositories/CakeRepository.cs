using CakeCrafter.Core.Interfaces;
using CakeCrafter.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    internal class CakeRepository : ICakeRepository
    {
        private readonly CakeCrafterDbContext _context;
        public CakeRepository(CakeCrafterDbContext context)
        {
            _context = context;
        }


        public async Task<List<Cake>> Get()
        {
            return await _context.Cakes.ToListAsync();
        }

        public async Task<Cake?> GetById(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return null;
            }

            return dbCake;
        }

        public async Task<Cake> Create(Cake cake)
        {
            _context.Cakes.Add(cake);
            await _context.SaveChangesAsync();
            return cake;
        }

        public async Task<Cake?> Update(Cake cake)
        {
            var dbCake = await _context.Cakes.FindAsync(cake.Id);
            if (dbCake == null)
            {
                return null;
            }

            _context.Cakes.Update(dbCake);
            await _context.SaveChangesAsync();
            return cake;
        }

        public async Task<bool> Delete(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return true;
            }
            _context.Cakes.Remove(dbCake);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
