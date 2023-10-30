using CakeCrafter.API.Models.Interfaces;

namespace CakeCrafter.API.Models
{
    public class MeasureUnit : IInfo
    {
        public int Id { get ; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
