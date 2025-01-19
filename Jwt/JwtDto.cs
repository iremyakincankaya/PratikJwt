namespace PratikJwt.Jwt
{
    public class JwtDto
    {
        public string Email { get; set; }
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
