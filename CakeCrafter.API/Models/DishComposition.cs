using Microsoft.Identity.Client;

namespace CakeCrafter.API.Models
{
    public class DishComposition
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientQuantity { get; set; }
    }
}
