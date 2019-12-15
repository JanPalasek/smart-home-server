using System;
using Microsoft.IdentityModel.Tokens;

namespace SmartHome.Web.Configurations
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public SecurityKey SecurityKey { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}