using System.Diagnostics.Eventing.Reader;

namespace CakeCrafter.API.Options
{
    public class JwtOptions
    {
        public static string SectionName = "JwtOptions";
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string SecretKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int TokenLifeTime {  get; set; }
    }
}
