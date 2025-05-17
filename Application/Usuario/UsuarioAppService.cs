using DesafioCCAA.Application.Usuario.DTO;
using DesafioCCAA.Domain.Interfaces;

namespace DesafioCCAA.Application.Usuario
{
    public class UsuarioAppService : IUsuarioAppService
    {
        IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<UsuarioViewModel> Cadastrar(UsuarioCadastroDTO usuario)
        {
            var usuarioDomain = new Domain.Entity.Usuario(usuario.Nome, usuario.Senha, usuario.Email, usuario.DataNascimento);
            var result = await _usuarioService.Cadastrar(usuarioDomain);

            if(result == null)
            {
                throw new Exception("Ouve um erro ao cadastrar o usuario!");
            }

            return new UsuarioViewModel(result);
        }

        public async Task<Guid> CriarTokenResetSenhaAsync(string email)
        {
            return await _usuarioService.CriarTokenResetSenhaAsync(email);
        }

        public async Task<UsuarioViewModel> EditarSenha(UsuarioResetSenhaDTO dto)
        {
            var result = await _usuarioService.EditarSenha(dto.IdUsuario, dto.Token, dto.NovaSenha);

            return new UsuarioViewModel(result);
        }
    }
}
