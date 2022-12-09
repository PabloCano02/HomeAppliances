namespace HomeAppliances.DTOs
{
    public class AuthenticationAnswer
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
