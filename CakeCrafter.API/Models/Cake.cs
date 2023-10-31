using CakeCrafter.API.Data;

namespace CakeCrafter.API.Models
{
    public class Cake
    {
        public int Id { get; set; }
        public List<CakesIngredients> CakeIngredients { get; set; } = new List<CakesIngredients>();
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TasteId { get; set; }
        //public Taste Taste { get; set; }
        public string TechnologyCard { get; set; }
        public decimal Cost
        {
            get
            {
                decimal cost = 0;
                var _dishes = CakeIngredients.Where(i => i.CakeId == Id);
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
