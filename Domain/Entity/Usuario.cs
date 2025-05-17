using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Entity
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string HashSenha { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public ICollection<Livro> Livros { get; set; }


        protected Usuario() { }

        public Usuario(string nome, string hashSenha, string email, DateTime dataNascimento)
        {
            Nome = nome;
            HashSenha = hashSenha;
            DataNascimento = dataNascimento;
            Email = email;
        }

        public void EditarSenha(string novaHashSenha)
        {
            HashSenha = novaHashSenha;
        }

        public void SetHasSenha(string token)
        {
            HashSenha = token;
        }
    }
}
