namespace CakeCrafter.Core.Models
{
    public class Cake
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid? ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public int? TasteId { get; set; }
        public int CookTimeInMinutes { get; set; }
        public int Level { get; set; }
        public double Weight { get; set; }
    }
}
