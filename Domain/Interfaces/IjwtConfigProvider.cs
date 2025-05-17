using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Interfaces
{
    public interface IjwtConfigProvider
    {
        string GetKey();
        string GetIssuer();
        string GetAudience();
        int GetExpiration();
    }
}
