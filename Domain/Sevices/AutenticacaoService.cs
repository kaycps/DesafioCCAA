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
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUnityOfWork _uow;
        private readonly ITokenService _tokenService;

        public AutenticacaoService(IUnityOfWork uow, ITokenService tokenService)
        {
            _uow = uow;
            _tokenService = tokenService;
        }

        public async Task<Usuario> Login(string email, string senha)
        {
           
            var usuario = await _uow.UsuarioRepository.BuscarPorEmailAsync(email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.HashSenha))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            var token = await _tokenService.GerarJWT(usuario);

            usuario.SetHasSenha(token);

            return usuario;
        }

       
    }
}
