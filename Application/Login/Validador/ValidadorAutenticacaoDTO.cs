using DesafioCCAA.Application.Login.DTO;
using DesafioCCAA.Application.Usuario.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Login.Validador
{
    public class ValidadorAutenticacaoDTO : AbstractValidator<AutenticacaoDTO>
    {
        public ValidadorAutenticacaoDTO()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(6);
        }
    }
}
