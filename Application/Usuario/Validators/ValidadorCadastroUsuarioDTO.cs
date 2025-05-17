using DesafioCCAA.Application.Usuario.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Usuario.Validators
{
    public class ValidadorCadastroUsuarioDTO : AbstractValidator<UsuarioCadastroDTO>
    {
        public ValidadorCadastroUsuarioDTO()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DataNascimento).NotEmpty().LessThan(DateTime.Today);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(6);
        }
    }
}
