using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeCrafter.API.Contracts
{
    public class CreateCakeRequest
    {
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [StringLength(200)]
        public string? Description { get; set; }
    }
}
