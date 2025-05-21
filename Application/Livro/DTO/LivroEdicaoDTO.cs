using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Livro.DTO
{
    public class LivroEdicaoDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Autor { get; set; }
        public string Sinopse { get; set; }
        public string FotoPath { get; set; }
        public int Genero { get; set; }
        public int Editora { get; set; }

        public Domain.Entity.Livro ToDomain()
        {
            return new Domain.Entity.Livro(Id, Titulo, ISBN, Autor, Sinopse, FotoPath, Genero, Editora);
        }

    }
}
