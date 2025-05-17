using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Entity
{
    public class Livro
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string ISBN { get; private set; }
        public string Autor {  get; private set; }
        public string Sinopse { get; private set; }
        public string FotoPath { get; private set; }
        public int IdGenero { get; private set; }

        public Editora Editora { get; private set; }
        public int IdEditora { get; private set; }

        public Guid IdUsuario { get; private set; }
        public Usuario Usuario { get; private set; }

        protected Livro() { }

        public Livro(string titulo, string isbn, string autor,  string sinopse, string pathFoto, int idGenero, Guid idUsuario, int idEditora)
        {
            Titulo = titulo;
            ISBN = isbn;
            Autor = autor;
            Sinopse = sinopse;
            FotoPath = pathFoto;
            IdGenero = idGenero;
            IdUsuario = idUsuario;
            IdEditora = idEditora;
        }

        public Livro(Guid id, string titulo, string isbn, string autor, string sinopse, string pathFoto, int idGenero, int idEditora)
        {
            Id = id;
            Titulo = titulo;
            ISBN = isbn;
            Autor = autor;
            Sinopse = sinopse;
            FotoPath = pathFoto;
            IdGenero = idGenero;
            IdEditora = idEditora;
        }

        public void SetTitulo(string titulo) { Titulo = titulo; }
        public void SetISBN(string isbn) { ISBN = isbn; }
        public void SetAutor(string autor) { Autor = autor; }
        public void SetSinopse(string sinopse) {  Sinopse = sinopse; }
        public void SetFotoPath(string pathFoto) { FotoPath = pathFoto; }
        public void SetIdGenero(int idGenero) { IdGenero = idGenero; }
        public void SetIdEditora(int idEditora) { IdEditora = idEditora; }


    }
}
