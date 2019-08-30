using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TicketingSystem.Core.JWT
{
    public class JwtFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly string _jwtExpiresIn;
        private readonly string _jwtKey;

        public JwtFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtKey = configuration.GetSection("JwtOptions").GetSection("Key").Value;
            _jwtExpiresIn = configuration.GetSection("JwtOptions").GetSection("ExpireDays").Value;
            _jwtIssuer = configuration.GetSection("JwtOptions").GetSection("Issuer").Value;
            _jwtAudience = configuration.GetSection("JwtOptions").GetSection("Audience").Value;
        }

        public string GenerateJwtToken(string userName, string userId, string[] roles)
        {
            List<Claim> claims = AddClaims(userName, userId, roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtExpiresIn));

            var token = new JwtSecurityToken(_jwtIssuer, _jwtAudience, claims, expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> AddClaims(string userName, string userId, string[] roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            return claims;
        }
    }
}
