namespace Modsen.Server.Shared.Models
{
    public class JwtOptions
    {
        public string ValidAudience { get; set; } = null!;
        public string ValidIssuer { get; set;} = null!;
        public string IssuerSigningKey { get; set; } = null!;
    }
}
