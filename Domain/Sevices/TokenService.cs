using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Sevices
{
    public class TokenService : ITokenService
    {
        private readonly IjwtConfigProvider _jwtConfigProvider;

        public TokenService(IjwtConfigProvider ijwtConfigProvider)
        {
            _jwtConfigProvider = ijwtConfigProvider;
        }

        public async Task<string> GerarJWT(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigProvider.GetKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _jwtConfigProvider.GetIssuer(),
                audience: _jwtConfigProvider.GetAudience(),
                expires: DateTime.UtcNow.AddMinutes(_jwtConfigProvider.GetExpiration()),
                signingCredentials: creds,
                claims: new[]
                {
                    new Claim("id", usuario.Id.ToString()),
                    new Claim("email", usuario.Email),
                    new Claim("name", usuario.Nome),
                }
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
