using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Name { get; set; }

        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

        public int IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public List<CakesIngredients> CakesIngredients { get; set; } = new List<CakesIngredients>();
    }
}
