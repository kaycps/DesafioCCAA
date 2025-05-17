using DesafioCCAA.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarJWT(Usuario usuario);
    }
}
