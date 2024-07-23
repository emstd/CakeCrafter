using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> Create(User user);
        Task<User> GetUserByEmail(string email);
    }
}
