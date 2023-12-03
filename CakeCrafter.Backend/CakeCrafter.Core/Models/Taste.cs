using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.Core.Models
{
    public class Taste
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
