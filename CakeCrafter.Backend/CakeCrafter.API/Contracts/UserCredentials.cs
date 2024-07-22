namespace CakeCrafter.API.Contracts
{
    public record UserCredentials
    {
        public required string Email {  get; set; }
        public required string Password { get; set; }
    }
}
