using DesafioCCAA.Application.Login.DTO;
using DesafioCCAA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Login
{
    public class AutenticacaoAppService : IAutenticacaoAppService
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoAppService(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        public async Task<ResultadoAutenticacaoDTO> Login(AutenticacaoDTO login)
        {
            var result = await _autenticacaoService.Login(login.Email, login.Senha);
            
            if (result == null)
            {
                throw new Exception("Erro ao efetuar login");
            }

            return new ResultadoAutenticacaoDTO()
            {
                Email = result.Email,
                Nome = result.Nome,
                Token = result.HashSenha
            };
            
        }
    }
}
