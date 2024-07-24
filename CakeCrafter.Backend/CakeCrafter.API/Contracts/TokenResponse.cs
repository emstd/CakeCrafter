namespace CakeCrafter.API.Contracts
{
    public record TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
