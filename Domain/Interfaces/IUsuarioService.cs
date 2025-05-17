using DesafioCCAA.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Cadastrar(Usuario usuario);
        Task<Guid> CriarTokenResetSenhaAsync(string email);
        Task<Usuario> EditarSenha(Guid idUsuario, string token, string senha);

    }
}
