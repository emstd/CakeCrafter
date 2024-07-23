using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<User> FindUserByEmail(string email); 
    }
}
