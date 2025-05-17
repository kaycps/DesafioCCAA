using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Usuario
{
    public class ResetSenhaRepository : IResetSenhaRepository
    {
        private readonly DesafioCCAAContext _context;

        public ResetSenhaRepository(DesafioCCAAContext context)
        {
            _context = context;
        }

        public async Task<ResetSenhaToken> CriarTokenResetAsync(ResetSenhaToken token)
        {
            await _context.AddAsync(token);
            return token;

        }

        public async Task<ResetSenhaToken?> ObterTokenValidoAsync(Guid idUsuario, string token)
        {
            var query = _context.ResetSenhaTokens
                        .Where(x => x.UsuarioId == idUsuario && x.DataExpiracao > DateTime.Now);

            if (!string.IsNullOrWhiteSpace(token))
            {
                query = query.Where(x => x.Token == token);
            }

            return await query.FirstOrDefaultAsync();

        }
    }
}
