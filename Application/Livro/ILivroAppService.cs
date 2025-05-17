using DesafioCCAA.Application.Livro.DTO;

namespace DesafioCCAA.Application.Livro
{
    public interface ILivroAppService
    {
        Task <LivroViewModel>AddAsync(Guid id, LivroCadastroDTO livro);       
        Task<IEnumerable<LivroViewModel>> BuscarAsync(Guid idUsuario,string termo);
        Task<LivroViewModel> UpdateAsync(LivroEdicaoDTO livro);
        Task<bool> DeleteAsync(Guid id);
        Task<byte[]> GerarRelatorioLivros(Guid idUsuario);
    }
}
