using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces
{
    public interface ICakeRepository
    {
        Task<List<Cake>> Get();

        Task<Cake?> GetById(int id);

        Task<Cake> Create(Cake cake);

        Task<Cake?> Update(Cake cake);

        Task<bool> Delete(int id);
    }
}
