using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OptiBid.Microservices.Contracts.Configuration;
using OptiBid.Microservices.Contracts.Services;

namespace OptiBid.Microservices.Services.Services
{
    public class JwtManager:IJwtManager
    {
        private readonly JwtSettings _jwtSettings;

        public JwtManager(IOptions<JwtSettings> options)
        {
            this._jwtSettings = options.Value;
        }
        public string GenerateToken(string username, string roleName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, roleName),
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpirationPeriod),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token) ;
        }
    }
}
