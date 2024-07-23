using AutoMapper;
using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CakeCrafterDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(CakeCrafterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Create(User user)
        {
            var userEntity = _mapper.Map<User, UserEntity>(user);
            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> FindUserByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (userEntity == null)
            {
                return null;
            }

            return _mapper.Map<UserEntity, User>(userEntity);
        }
    }
}
