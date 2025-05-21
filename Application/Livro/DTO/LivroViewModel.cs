using DesafioCCAA.Application.Genero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Livro.DTO
{
    public class LivroViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Autor { get; set; }
        public string Sinopse { get; set; }
        public string FotoPath { get; set; }
        public int Genero { get; set; }
        public int Editora { get; set; }
        

        public LivroViewModel(Domain.Entity.Livro livro)
        {
            Id = livro.Id;
            Titulo = livro.Titulo;
            ISBN = livro.ISBN;
            Autor = livro.Autor;
            Sinopse = livro.Sinopse;
            FotoPath = livro.FotoPath;

            Genero = livro.IdGenero;

            Editora = livro.IdEditora;
        }
    }
}
