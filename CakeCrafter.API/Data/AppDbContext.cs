using CakeCrafter.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Taste> Tastes { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<CakesIngredients> CakesIngredients { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CakesIngredients>()
                .HasKey(ci => new { ci.CakeId, ci.IngredientId });
        }
    }
}
