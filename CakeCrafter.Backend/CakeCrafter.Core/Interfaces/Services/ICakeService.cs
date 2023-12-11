using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Services
{
    public interface ICakeService
    {
        Task<List<Cake>> Get(int categoryId, int skip, int take);

        Task<Cake?> GetById(int id);

        Task<int> Create(Cake cake);

        Task<Cake?> Update(Cake cake);

        Task<bool> Delete(int id);
    }
}
