using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Infrastructure.Repository.Editora;
using DesafioCCAA.Infrastructure.Repository.Livro;
using DesafioCCAA.Infrastructure.Repository.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository
{
    public class UnityOfWorkRepository : IUnityOfWork
    {
        public DesafioCCAAContext _context;
        private IUsuarioRepository _usuarioRepository;
        private ILivroRepository _livroRepository;
        private IEditoraRepository _editoraRepository;
        private IResetSenhaRepository _resetSenhaRepository;

        public UnityOfWorkRepository(DesafioCCAAContext context)
        {
            _context = context;
        }        

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                return _usuarioRepository = _usuarioRepository ?? new UsuarioRepository(_context); 
            }
        }

        public ILivroRepository LivroRepository
        {
            get
            {
                return _livroRepository = _livroRepository ?? new LivroRepository(_context);
            }
        }

        public IEditoraRepository EditoraRepository
        {
            get
            {
                return _editoraRepository = _editoraRepository ?? new EditoraRepository(_context);
            }
        }

        public IResetSenhaRepository ResetSenhaRepository
        {
            get
            {
                return _resetSenhaRepository = _resetSenhaRepository ?? new ResetSenhaRepository(_context);
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
