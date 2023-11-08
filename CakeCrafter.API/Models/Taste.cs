namespace CakeCrafter.API.Models
{
    public class Taste
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cake> dishes { get; set; }
    }
}
