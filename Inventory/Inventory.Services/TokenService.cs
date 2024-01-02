using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory.Entities;
using Inventory.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _sskey;

        public TokenService(IConfiguration configuration)
        {
            _sskey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["tokenKey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Email));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim("Email", user.Email));

            var credentials = new SigningCredentials(_sskey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer              = "issuer",
                Audience            = "issuer",
                Subject             = new(claims),
                SigningCredentials  = credentials,
                Expires             = DateTime.UtcNow.AddHours(2)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}