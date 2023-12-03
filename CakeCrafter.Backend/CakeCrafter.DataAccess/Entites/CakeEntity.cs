using CakeCrafter.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.DataAccess.Entites
{
    public class CakeEntity
    {
        public int Id { get; set; }
        public List<CakesIngredients> CakeIngredients { get; set; } = new List<CakesIngredients>();

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int TasteId { get; set; }
        public Taste Taste { get; set; }

        public decimal Cost
        {
            get
            {
                decimal cost = 0;
                var cakes = CakeIngredients.Where(i => i.CakeId == Id);
                foreach (var cake in cakes)
                {
                    cost += cake.IngredientQuantity * cake.Ingredient.Price;
                }
                return cost;
            }
        }
        public TimeSpan CookTime { get; set; }

        public int Level { get; set; }

        public double Weight { get; set; }
    }
}
