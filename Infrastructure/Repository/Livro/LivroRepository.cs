using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Livro
{
    public class LivroRepository : ILivroRepository
    {

        private readonly DesafioCCAAContext _context;

        public LivroRepository(DesafioCCAAContext context)
        {
            _context = context;
        }

        public  Task AtualizarAsync(Domain.Entity.Livro book)
        {
            _context.Livros.Update(book);
            return Task.CompletedTask;
        }

        public async Task<Domain.Entity.Livro> BucarPorIdAsync(Guid id)
        {
            return await _context.Livros.Include(x=>x.Editora).FirstAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Domain.Entity.Livro>> BuscarAsync(Guid idUsuario, string query)
        {
            var livrosQuery = _context.Livros
                .Include(x => x.Editora)
                .Where(x => x.IdUsuario == idUsuario)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.ToLower();

                livrosQuery = livrosQuery.Where(x =>
                    x.Titulo.ToLower().Contains(query) ||
                    x.ISBN.ToLower().Contains(query) ||
                    x.Autor.ToLower().Contains(query) ||
                    x.Editora.Nome.ToLower().Contains(query)
                // || x.Genero.ToLower().Contains(query)
                );
            }

            return await livrosQuery.ToListAsync();
            
        }

        public async Task<IEnumerable<Domain.Entity.Livro>> BuscarPorUsuarioAsync(Guid idUsuario)
        {
            return await _context.Livros.Where(b => b.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<Domain.Entity.Livro> CadastrarAsync(Domain.Entity.Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            return livro;

        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            return true;
        }
    }
}
