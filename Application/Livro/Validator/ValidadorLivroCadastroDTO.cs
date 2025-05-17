using DesafioCCAA.Application.Livro.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Livro.Validator
{
    public class ValidadorLivroCadastroDTO : AbstractValidator<LivroCadastroDTO>
    {
        public ValidadorLivroCadastroDTO()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ISBN).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Autor).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Sinopse).NotEmpty().MaximumLength(5000);
            RuleFor(x => x.Editora).NotEmpty();
            RuleFor(x => x.Genero).NotEmpty();
        }
    }
}
