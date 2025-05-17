using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DesafioCCAAContext _context;

        public UsuarioRepository(DesafioCCAAContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entity.Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Domain.Entity.Usuario?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Domain.Entity.Usuario> CadastrarAsync(Domain.Entity.Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            return usuario;
        }
    }
}
