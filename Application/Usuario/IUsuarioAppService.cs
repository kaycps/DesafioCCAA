using DesafioCCAA.Application.Usuario.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Usuario
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> Cadastrar(UsuarioCadastroDTO usuario);
        Task<Guid> CriarTokenResetSenhaAsync(string email);
        Task<UsuarioViewModel> EditarSenha(UsuarioResetSenhaDTO dTO);


    }
}
