using CakeCrafter.API.Models.Interfaces;
using Microsoft.Identity.Client;

namespace CakeCrafter.API.Models
{
    public class IngredientCategory : IInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
