using DesafioCCAA.Application.Login.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Login
{
    public interface IAutenticacaoAppService
    {
        Task<ResultadoAutenticacaoDTO> Login(AutenticacaoDTO login);

    }
}
