using DesafioCCAA.Infrastructure.Repository.Livro;
using DesafioCCAA.Infrastructure.Repository.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }
        ILivroRepository LivroRepository { get; }
        IEditoraRepository EditoraRepository { get; }
        IResetSenhaRepository ResetSenhaRepository { get; }
        Task<int> CommitAsync();
    }
}
