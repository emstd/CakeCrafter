using System.ComponentModel.DataAnnotations.Schema;

namespace CakeCrafter.DataAccess.Entites
{
    public class ImageEntity
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? Extension { get; set; }
    }
}
