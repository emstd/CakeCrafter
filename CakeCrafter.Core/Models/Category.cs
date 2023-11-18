using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCrafter.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string? Name { get; set; }

        public List<Cake> Cakes { get; set; } = new List<Cake>();
    }
}
