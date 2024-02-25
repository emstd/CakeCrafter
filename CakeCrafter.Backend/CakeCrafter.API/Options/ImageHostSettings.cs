namespace CakeCrafter.API.Options
{
    public class ImageHostSettings
    {
        public required Uri ImageHostUrl { get; set; }
        public static string SectionName { get; } = "ImageHostSettings";
    }
}
