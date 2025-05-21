using DesafioCCAA.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Interfaces
{
    public interface ILivroSevice
    {
        Task<Livro> AddAsync(Livro livro);
        Task<IEnumerable<Livro>> BuscarAsync(Guid idUsuario, string termo);
        Task<Livro> UpdateAsync(Livro livro);
        Task<Livro> Buscar(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
