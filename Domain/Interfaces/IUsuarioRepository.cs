using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        Task<Domain.Entity.Usuario?> BuscarPorEmailAsync(string email);
        Task<Domain.Entity.Usuario?> BuscarPorIdAsync(Guid id);
        Task <Domain.Entity.Usuario> CadastrarAsync(Domain.Entity.Usuario usuario);
    }
}

