using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.API.Contracts
{
    public record LoginRequest
    {
        [EmailAddress]
        public required string Email {  get; set; }
        public required string Password { get; set; }
    }
}
