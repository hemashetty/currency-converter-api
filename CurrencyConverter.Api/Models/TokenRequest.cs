namespace CurrencyConverter.Api.Models
{
    public class TokenRequest
    {
        public string ClientId { get; set; }
        public string Role { get; set; } // Admin or User
    }
}
