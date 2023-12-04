using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly CakeCrafterDbContext _context;
        public CakeRepository(CakeCrafterDbContext context)
        {
            _context = context;
        }


        public async Task<List<Cake>> Get(int categoryId, int skip, int take)
        {
            var cakes = await _context.Cakes
                                      .Where(cake => cake.CategoryId == categoryId)
                                      .Skip(skip)
                                      .Take(take)
                                      .Select(cake => new Cake
                                      {
                                          Id = cake.Id,
                                          Name = cake.Name,
                                          Description = cake.Description,
                                          CookTimeInMinutes = cake.CookTimeInMinutes,
                                          Level = cake.Level,
                                          Weight = cake.Weight,
                                          CategoryId = cake.CategoryId,
                                          TasteId = cake.TasteId,
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

            var result = new Cake
            {
                Id = dbCake.Id,
                Name = dbCake.Name,
                Description = dbCake.Description,
                CategoryId = dbCake.CategoryId,
                TasteId = dbCake.TasteId,
                CookTimeInMinutes = dbCake.CookTimeInMinutes,
                Level = dbCake.Level,
                Weight = dbCake.Weight,
            };

            return result;
        }

        public async Task<int> Create(Cake cake)
        {
            var dbCake = new CakeEntity
            {
                Name = cake.Name,
                Description = cake.Description,
                CategoryId = cake.CategoryId,
                TasteId = cake.TasteId,
                CookTimeInMinutes = cake.CookTimeInMinutes,
                Level = cake.Level,
                Weight = cake.Weight,
            };
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

            dbCake = new CakeEntity
            {
                Name = cake.Name,
                Description = cake.Description,
                CategoryId = cake.CategoryId,
                TasteId = cake.TasteId,
                CookTimeInMinutes = cake.CookTimeInMinutes,
                Level = cake.Level,
                Weight = cake.Weight,
            };

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
