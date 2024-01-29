namespace CakeCrafter.API.Contracts
{
    public record CakeUpdateRequest
    {
        public IFormFile? Image { get; set; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public int TasteId { get; init; }

        public int CategoryId { get; init; }

        public int CookTimeInMinutes { get; init; }

        public int Level { get; init; }

        public double Weight { get; init; }
    }
}
