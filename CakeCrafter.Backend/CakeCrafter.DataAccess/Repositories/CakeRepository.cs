﻿using AutoMapper;
using Azure;
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
                                                        .Where(cake => cake.CategoryId == categoryId)
                                                        .ToArrayAsync();                //получаем все изделия в категории

            var page = new ItemsPage<Cake>
            {
                Items = queryCakesInCategory.Skip(skip)                                 //берем столько изделий из категории,
                                            .Take(take)                                 //сколько указано в запросе
                                            .Select(_mapper.Map<CakeEntity, Cake>)
                                            .ToArray(),                               

                TotalItems = queryCakesInCategory.Length                                //указываем, солько всего
            };                                                                          //изделий в категории
            return page;
        }

        public async Task<Cake?> GetById(int id)
        {
            var dbCake = await _context.Cakes.AsNoTracking().FirstOrDefaultAsync(cake => cake.Id == id);
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
            await _context.Cakes.Where(cake => cake.Id == id).ExecuteDeleteAsync();
            return true;
        }
    }
}
