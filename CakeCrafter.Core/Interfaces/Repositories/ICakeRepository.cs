using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Repositories
{
    public interface ICakeRepository
    {
        Task<List<Cake>> Get(string category, int pageNumber);

        Task<Cake?> GetById(int id);

        Task<Cake> Create(Cake cake);

        Task<Cake?> Update(Cake cake);

        Task<bool> Delete(int id);
    }
}
