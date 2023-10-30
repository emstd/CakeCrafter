using Microsoft.Identity.Client;

namespace CakeCrafter.API.Models
{
    public class CakesIngredients
    {
        public int DishId { get; set; }
        public Cake Dish { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientQuantity { get; set; }
    }
}
