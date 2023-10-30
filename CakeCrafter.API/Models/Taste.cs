using CakeCrafter.API.Models.Interfaces;

namespace CakeCrafter.API.Models
{
    public class Taste : IInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cake> dishes { get; set; }
    }
}
