using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesafioCCAA.Infrastructure.JwtConfigProvider;

namespace DesafioCCAA.Infrastructure
{
    public class JwtConfigProvider : IjwtConfigProvider
    {
        private readonly JwtConfig _config;

        public JwtConfigProvider(IOptions<JwtConfig> options)
        {
            _config = options.Value;
        }

        public string GetKey() => _config.Key;
        public string GetIssuer() => _config.Issuer;

        public string GetAudience() => _config.Audience;

        public int GetExpiration() => _config.ExpirationInMinutes;
    }
}
