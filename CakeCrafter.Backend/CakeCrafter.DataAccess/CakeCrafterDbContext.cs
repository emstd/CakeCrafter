using CakeCrafter.DataAccess.Cofigurations;
using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.DataAccess
{
    public class CakeCrafterDbContext : DbContext
    {
        public CakeCrafterDbContext(DbContextOptions<CakeCrafterDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TasteEntity> Tastes { get; set; }
        public DbSet<CakeEntity> Cakes { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
