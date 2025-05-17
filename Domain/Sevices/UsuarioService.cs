using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Infrastructure.Repository.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Sevices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnityOfWork _uow;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUnityOfWork uow, ITokenService token)
        {
            _uow = uow;
            _tokenService = token;
        }

        public async Task<Usuario> Cadastrar(Usuario usuario)
        {
            var exists = await _uow.UsuarioRepository.BuscarPorEmailAsync(usuario.Email);
            if (exists != null)
                throw new ApplicationException("Usuário já cadastrado.");

            var hashSenha = BCrypt.Net.BCrypt.HashPassword(usuario.HashSenha);
            usuario.SetHasSenha(hashSenha);

            var result = await _uow.UsuarioRepository.CadastrarAsync(usuario);
            await _uow.CommitAsync();

            return  result;
        }

        public async Task<Guid> CriarTokenResetSenhaAsync(string email)
        {
            var usuario = await _uow.UsuarioRepository.BuscarPorEmailAsync(email);

            if (usuario == null || email != usuario.Email)
                throw new ArgumentException("Usuario inválido!");

            var tokenValidoExistente = await _uow.ResetSenhaRepository.ObterTokenValidoAsync(usuario.Id, "");

            if(tokenValidoExistente == null)
            {
                string token = GerarTokenResetSenha();

                var resetToken = new ResetSenhaToken
                {
                    UsuarioId = usuario.Id,
                    Token = token,
                    DataExpiracao = DateTime.UtcNow.AddHours(1)
                };

                var result = await _uow.ResetSenhaRepository.CriarTokenResetAsync(resetToken);

                if (result == null)
                {
                    throw new ArgumentException("Não foi possivel criar o token para recuperar a senha!");
                }

                await _uow.CommitAsync();
                tokenValidoExistente = result;
            }            

            var smtp = new SmtpClient("smtp.ethereal.email")
            {
                Port = 587,
                Credentials = new NetworkCredential("cletus.lakin82@ethereal.email", "33mXtXAPPZ5w7vtCrZ"),
                EnableSsl = true,
            };

            var mensagem = new MailMessage("cletus.lakin82@ethereal.email", usuario.Email, "Assunto", tokenValidoExistente.Token);

            await smtp.SendMailAsync(mensagem);

            return usuario.Id;
        }

        public async Task<Usuario> EditarSenha(Guid idUsuario, string token, string senha)
        {
            var usuario = await _uow.UsuarioRepository.BuscarPorIdAsync(idUsuario);
            if (usuario == null)
            {
                throw new ArgumentException("usuario inválido!");
            }

            var tokenValido = await _uow.ResetSenhaRepository.ObterTokenValidoAsync(idUsuario, token);
            if (tokenValido == null)
            {
                throw new ArgumentException("token inválido!");
            }

            usuario.EditarSenha(BCrypt.Net.BCrypt.HashPassword(senha));

            await _uow.CommitAsync();
            return usuario;
        }

        private string GerarTokenResetSenha()
        {
            var random = new Random();
            string codigo = random.Next(0, 10000).ToString("D4");

            return codigo;
        }
    }
}
