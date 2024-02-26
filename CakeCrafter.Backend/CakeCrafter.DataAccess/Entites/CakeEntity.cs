using System.ComponentModel.DataAnnotations.Schema;

namespace CakeCrafter.DataAccess.Entites
{
    public class CakeEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public required string Description { get; set; }

        public Guid? ImageId { get; set; }
        public ImageEntity? Image { get; set; }

        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }

        public int? TasteId { get; set; }
        public TasteEntity? Taste { get; set; }

        public int CookTimeInMinutes { get; set; }
        public int Level { get; set; }
        public double Weight { get; set; }
    }
}
