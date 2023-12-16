using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;

namespace CakeCrafter.Core.Interfaces.Repositories
{
    public interface ICakeRepository
    {
        Task<ItemsPage<Cake>> Get(int categoryId, int skip, int take);

        Task<Cake?> GetById(int id);

        Task<int> Create(Cake cake);

        Task<Cake?> Update(Cake cake);

        Task<bool> Delete(int id);
    }
}
