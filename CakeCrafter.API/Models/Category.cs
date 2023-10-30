using CakeCrafter.API.Models.Interfaces;

namespace CakeCrafter.API.Models
{
    public class Category : IInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cake> Dishes { get; set; }  = new List<Cake>();
    }
}
