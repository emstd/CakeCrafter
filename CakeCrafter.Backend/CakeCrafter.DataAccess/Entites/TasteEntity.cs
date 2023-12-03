using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.DataAccess.Entites
{
    public class TasteEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Name { get; set; }
    }
}
