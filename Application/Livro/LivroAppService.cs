using DesafioCCAA.Application.Livro.DTO;
using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Enum;
using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Infrastructure.Repository.Livro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Livro
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroSevice _livroSevice;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IPdfService _pdfService;

        public LivroAppService(ILivroSevice livroSevice, IUnityOfWork unityOfWork, IPdfService pdfService)
        {
            _livroSevice = livroSevice;
            _unityOfWork = unityOfWork;
            _pdfService = pdfService;
        }

        public async Task<LivroViewModel> AddAsync(Guid id, LivroCadastroDTO livro)
        {
            ValidarGenero(livro.Genero);

            var result = await _livroSevice.AddAsync(livro.ToDomain(id));
            if(result == null)
            {
                return null;
            }

            return new LivroViewModel(result);
        }

        public async Task<IEnumerable<LivroViewModel>> BuscarAsync(Guid idUsuario, string termo)
        {
            var livros = await _livroSevice.BuscarAsync(idUsuario, termo);

            return livros.Select(livro => new LivroViewModel(livro));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _livroSevice.DeleteAsync(id);

        }

        public async Task<byte[]> GerarRelatorioLivros(Guid idUsuario)
        {
            var livros = await _livroSevice.BuscarAsync(idUsuario, "");

            return await _pdfService.GerarRelatorioLivros(livros);
        }       

        public async Task<LivroViewModel> UpdateAsync(LivroEdicaoDTO livro)
        {
            ValidarGenero(livro.Genero);

            var result = await _livroSevice.UpdateAsync(livro.ToDomain());

            return new LivroViewModel(result);
        }

        public void ValidarGenero(int idGenero)
        {
            var genero = Enum.GetName(typeof(EGenero), idGenero);
            if (genero == null)
            {
                throw new ArgumentException("Genero invalido!");

            }
        }
    }
}
