using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.Domain
{
    public class CakeService : ICakeService
    {
        private readonly ICakeRepository _repository;

        public CakeService(ICakeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cake>> Get(string category, int pageNumber)
        {
            return await _repository.Get(category, pageNumber);
        }

        public Task<Cake?> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<Cake> Create(Cake cake)
        {
            return await _repository.Create(cake);
        }

        public async Task<Cake?> Update(Cake cake)
        {
            return await _repository.Update(cake);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}