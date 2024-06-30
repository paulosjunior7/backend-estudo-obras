namespace Obras.Api.Models
{
    using System;

    public class TokenDetails
    {
        public string UserId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
