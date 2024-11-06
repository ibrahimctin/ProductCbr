namespace ProductCbrAssignment.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string JWT = "JWT";
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string Secret { get; set; } = default!;
        public double AccessTokenExpireTime { get; set; }
    }
}
