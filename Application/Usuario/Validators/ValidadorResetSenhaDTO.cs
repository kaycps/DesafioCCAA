using DesafioCCAA.Application.Usuario.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Usuario.Validators
{
    public class ValidadorResetSenhaDTO : AbstractValidator<UsuarioResetSenhaDTO>
    {
        public ValidadorResetSenhaDTO()
        {
            RuleFor(x => x.IdUsuario).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.NovaSenha).NotEmpty().MinimumLength(6);
        }
    }
}
