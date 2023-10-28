using CakeCrafter.API.Data;

namespace CakeCrafter.API.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public int DishCompositionId { get; set; }
        public List<DishComposition> DishComposition { get; set; } = new List<DishComposition>();
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TasteId { get; set; }
        public Taste Taste { get; set; }
        public string TechnologyCard { get; set; }
        public decimal Cost
        {
            get
            {
                decimal cost = 0;
                var _dishes = DishComposition.Where(i => i.DishId == Id);
                foreach (var dish in _dishes)
                {
                    cost += dish.IngredientQuantity * dish.Ingredient.Price;
                }
                return cost;
            }
        }
        public TimeSpan CookTime { get; set; }
        public int Level { get; set; }
    }
}
