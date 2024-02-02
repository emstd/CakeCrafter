namespace CakeCrafter.API.Contracts
{
    public class CakeGetByIdResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public string? ImageURL { get; set; } = null;
        public Guid? ImageId { get; set; }
        public int? TasteId { get; init; }

        public int? CategoryId { get; init; }

        public int CookTimeInMinutes { get; init; }

        public int Level { get; init; }

        public double Weight { get; init; }
    }
}
