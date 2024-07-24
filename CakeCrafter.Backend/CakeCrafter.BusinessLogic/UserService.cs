using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public (string, string) ConfirmAccess()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(User user)
        {
            var result = await _repository.Create(user);
            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _repository.FindUserByEmail(email);
            return user;
        }
    }
}
