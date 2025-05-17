using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Entity
{
    public class Editora
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        protected Editora() { }

        public Editora(string nome)
        {
            Nome = nome;
        }
    }
}
