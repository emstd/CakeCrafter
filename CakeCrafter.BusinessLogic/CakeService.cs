using CakeCrafter.Core.Interfaces;
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

        public async Task<List<Cake>> Get()
        {
            return await _repository.Get();
        }

        public Task<Cake?> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<Cake> Create(Cake cake)
        {
            await _repository.Create(cake);
            return cake;
        }

        public async Task<Cake?> Update(Cake cake)
        {
            var _cake = await _repository.Update(cake);
            if (_cake == null)
            {
                return null;
            }

            return _cake;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result;
        }
    }
}