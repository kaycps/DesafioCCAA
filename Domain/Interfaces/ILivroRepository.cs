using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Livro
{
    public interface ILivroRepository
    {
        Task<Domain.Entity.Livro> BucarPorIdAsync(Guid id);
        Task<IEnumerable<Domain.Entity.Livro>> BuscarPorUsuarioAsync(Guid userId);
        Task<IEnumerable<Domain.Entity.Livro>> BuscarAsync( Guid userId, string query);
        Task<Domain.Entity.Livro> CadastrarAsync(Domain.Entity.Livro book);
        Task AtualizarAsync(Domain.Entity.Livro book);
        Task<bool> DeletarAsync(Guid id);
    }
}
