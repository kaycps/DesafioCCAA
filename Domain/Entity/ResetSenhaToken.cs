using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Entity
{
    public class ResetSenhaToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public bool Utilizado { get; set; } = false;

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
