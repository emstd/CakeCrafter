using System.ComponentModel.DataAnnotations.Schema;

namespace CakeCrafter.API.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

        public int IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public List<CakesIngredients> DishComposition { get; set; } = new List<CakesIngredients>();
    }
}
