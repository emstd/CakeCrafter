using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private const int AmountOfElements = 5;
        private readonly CakeCrafterDbContext _context;
        public CakeRepository(CakeCrafterDbContext context)
        {
            _context = context;
        }


        public async Task<List<Cake>> Get(string category, int PageNumber)
        {
                var cakes = await _context.Cakes
                                          .Where(cake => cake.Category.Name == category)
                                          .Skip((PageNumber - 1) * AmountOfElements)
                                          .Take(AmountOfElements)
                                          .Select(cake => new Cake
                                          {
                                              Id = cake.Id,
                                              Name = cake.Name,
                                              Description = cake.Description,
                                              CookTime = cake.CookTime,
                                              Level = cake.Level,
                                              Weight = cake.Weight,
                                              Category = cake.Category,
                                              Taste = cake.Taste,
                                          })
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
                return false;
            }
            _context.Cakes.Remove(dbCake);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
