using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.Core.Models
{
    public class CakesIngredients
    {
        [Key, Column(Order = 1)]
        public int CakeId { get; set; }

        public Cake Cake { get; set; }


        [Key, Column(Order = 2)]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public int IngredientQuantity { get; set; }
    }
}
