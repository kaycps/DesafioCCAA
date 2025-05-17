using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Usuario.DTO
{
    public class UsuarioResetSenhaDTO
    {
        public Guid IdUsuario { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
