using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CakeCrafter.Core.Models
{
    public class IngredientCategory
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string? Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
