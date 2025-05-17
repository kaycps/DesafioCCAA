using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Enum;
using DesafioCCAA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Sevices
{
    public class LivroService : ILivroSevice
    {
        private readonly IUnityOfWork _unityOfWork;

        public LivroService(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Livro> AddAsync(Livro livro)
        {
            var editora = await _unityOfWork.EditoraRepository.GetByIdAsync(livro.IdEditora);
            if (editora == null)
            {
                throw new ArgumentException("Editora invalida!");
            }

            livro.SetIdEditora(editora.Id);

            var result = await _unityOfWork.LivroRepository.CadastrarAsync(livro);
            await _unityOfWork.CommitAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _unityOfWork.LivroRepository.DeletarAsync(id);
            if (result)
            {
                await _unityOfWork.CommitAsync();
            }
            
            return result;
        }

        public async Task<IEnumerable<Livro>> BuscarAsync(Guid idUsuario, string termo)
        {
            return await _unityOfWork.LivroRepository.BuscarAsync(idUsuario, termo);
        }

        public async Task<Livro> UpdateAsync(Livro livro)
        {
            var livroCadastrado = await _unityOfWork.LivroRepository.BucarPorIdAsync(livro.Id);
            if(livroCadastrado == null)
            {
                throw new ArgumentException("Livro invalida!");
            }

            var editora = await _unityOfWork.EditoraRepository.GetByIdAsync(livro.IdEditora);
            if(editora == null)
            {
                throw new ArgumentException("Editora invalida!");
            }

            livroCadastrado.SetTitulo(livro.Titulo);
            livroCadastrado.SetAutor(livro.Autor);
            livroCadastrado.SetSinopse(livro.Sinopse);
            livroCadastrado.SetFotoPath(livro.FotoPath);
            livroCadastrado.SetISBN(livro.ISBN);
            livroCadastrado.SetIdGenero(livro.IdGenero);
            livroCadastrado.SetIdEditora(livro.IdEditora);

            await _unityOfWork.LivroRepository.AtualizarAsync(livroCadastrado);
            await _unityOfWork.CommitAsync();

            return livroCadastrado;
        }
    }
}
