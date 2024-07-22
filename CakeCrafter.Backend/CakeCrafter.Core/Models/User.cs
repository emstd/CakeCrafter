using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCrafter.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } //Конечно потом на хэш переделать
    }
}
