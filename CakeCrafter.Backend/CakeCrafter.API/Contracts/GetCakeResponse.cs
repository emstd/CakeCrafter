using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Contracts
{
    public class GetCakeResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int TasteId { get; set; }

        public int CategoryId { get; set; }

        public TimeSpan CookTime { get; set; }

        public int Level { get; set; }

        public double Weight { get; set; }
    }
}
