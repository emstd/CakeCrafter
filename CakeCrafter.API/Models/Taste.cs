namespace CakeCrafter.API.Models
{
    public class Taste
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dish> dishes { get; set; }
    }
}
