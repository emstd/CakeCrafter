namespace CakeCrafter.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<TModel>> Get();

        Task<TModel?> GetById(int id);

        Task<TModel> Create(TModel model);

        Task<TModel?> Update(TModel model, int id);

        Task<bool> Delete(int id);
    }
}
