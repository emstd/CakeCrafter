namespace CakeCrafter.API.Models
{
    public class MeasureUnit
    {
        public int Id { get ; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
