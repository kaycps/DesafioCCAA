using DesafioCCAA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure.Repository.Editora
{
    public class EditoraRepository : IEditoraRepository
    {
        private readonly DesafioCCAAContext _context;

        public EditoraRepository(DesafioCCAAContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entity.Editora> GetByIdAsync(int id)
        {
            return await _context.Editora.FirstAsync(u => u.Id == id);
        }

        public async Task<Domain.Entity.Editora> GetByNomeAsync(string nome)
        {
            return await _context.Editora.FirstAsync(u => u.Nome == nome);
        }
    }
}
