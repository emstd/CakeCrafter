using CakeCrafter.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.DataAccess.Entites
{
    public class CategoryEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Name { get; set; }
    }
}
