using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;

namespace CakeCrafter.BusinessLogic
{
    public class GenericService<TModel> : IGenericService<TModel> where TModel : class
    {

        private readonly IGenericRepository<TModel> _repository;

        public GenericService(IGenericRepository<TModel> repository)
        {
            _repository = repository;
        }


        public async Task<TModel> Create(TModel model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<TModel>> Get()
        {
            return await _repository.Get();
        }

        public async Task<TModel?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TModel?> Update(TModel model, int id)
        {
            return await _repository.Update(model, id);
        }
    }
}
