namespace CakeCrafter.DataAccess.Repositories
{
    public class UserRepository
    {
        public CakeCrafterDbContext _context { get; }
        public UserRepository(CakeCrafterDbContext context)
        {
            _context = context;
        }


    }
}
