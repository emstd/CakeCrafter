using AutoMapper;
using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
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


        public async Task<ItemsPage<Cake>> Get(int categoryId, int skip, int take)
        {
            var queryCakesInCategory = await _context.Cakes.AsNoTracking()
                                                           .Include(C => C.Image)
                                                           .Where(cake => cake.CategoryId == categoryId)
                                                           .Skip(skip)
                                                           .Take(take)
                                                           .ToArrayAsync();                //получаем все изделия в категории
             
            foreach (var cake in queryCakesInCategory)
            {
                if(cake.ImageId == null)
                {
                    cake.ImageUrl = null;
                }
                else
                {
                    cake.ImageUrl = cake.Image.Id.ToString() + cake.Image.Extension;
                }
            }

            var page = new ItemsPage<Cake>
            {
                Items = queryCakesInCategory.Select(_mapper.Map<CakeEntity, Cake>)
                                            .ToArray(),

                TotalItems = queryCakesInCategory.Length                                //указываем, солько всего
            };                                                                          //изделий в категории
            return page;
        }

        public async Task<Cake?> GetById(int id)
        {
            var cake = await _context.Cakes.AsNoTracking()
                                             .Include(cake => cake.Image)
                                             .FirstOrDefaultAsync(cake => cake.Id == id);
            if (cake == null)
            {
                return null;
            }
            if (cake.ImageId == null)
            {
                cake.ImageUrl = null;
            }
            else
            {
                cake.ImageUrl = cake.Image.Id.ToString() + cake.Image.Extension;
            }
            var result = _mapper.Map<CakeEntity, Cake>(cake);

            return result;
        }

        public async Task<int> Create(Cake cake)
        {
            var dbCake = _mapper.Map<Cake, CakeEntity>(cake);
            await _context.Cakes.AddAsync(dbCake);
            await _context.SaveChangesAsync();

            return dbCake.Id;
        }

        public async Task<Cake?> Update(Cake cake)
        {
            var dbCake = await _context.Cakes.AsNoTracking().FirstOrDefaultAsync(dbCake => dbCake.Id == cake.Id);
            if (dbCake == null)
            {
                return null;
            }

            dbCake = _mapper.Map<Cake, CakeEntity>(cake);

            _context.Cakes.Update(dbCake);
            await _context.SaveChangesAsync();
            //Нужно подумать, что тут возвращать
            return cake;
        }

        public async Task<bool> Delete(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return false;
            }
            await _context.Cakes.Where(cake => cake.Id == id).ExecuteDeleteAsync();
            return true;
        }
    }
}
