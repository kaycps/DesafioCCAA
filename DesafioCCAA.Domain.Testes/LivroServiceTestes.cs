using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Domain.Sevices;
using DesafioCCAA.Infrastructure.Repository.Livro;
using Moq;

namespace DesafioCCAA.Domain.Testes
{
    public class LivroServiceTestes
    {
        private readonly Mock<IUnityOfWork> _uowMock;
        private readonly Mock<ILivroRepository> _livroRepoMock;
        private readonly Mock<IEditoraRepository> _editoraRepoMock;
        private readonly LivroService _service;

        public LivroServiceTestes()
        {
            _uowMock = new Mock<IUnityOfWork>();
            _livroRepoMock = new Mock<ILivroRepository>();
            _editoraRepoMock = new Mock<IEditoraRepository>();

            _uowMock.Setup(u => u.LivroRepository).Returns(_livroRepoMock.Object);
            _uowMock.Setup(u => u.EditoraRepository).Returns(_editoraRepoMock.Object);

            _service = new LivroService(_uowMock.Object);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarLivro_QuandoValido()
        {
            // Arrange
            var livro = new Livro("Título", "ISBN", "Autor", "Sinopse", "foto.jpg", 1, Guid.NewGuid(), 1);
            var editora = new Editora("Nome da Editora");
            typeof(Editora).GetProperty("Id").SetValue(editora, livro.IdEditora);

            _editoraRepoMock.Setup(r => r.GetByIdAsync(livro.IdEditora)).ReturnsAsync(editora);
            _livroRepoMock.Setup(r => r.CadastrarAsync(livro)).ReturnsAsync(livro);

            // Act
            var result = await _service.AddAsync(livro);

            // Assert
            Assert.Equal(livro, result);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_DeveLancarExcecao_QuandoEditoraInvalida()
        {
            // Arrange
            var livro = new Livro("Título", "ISBN", "Autor", "Sinopse", "foto.jpg", 1, Guid.NewGuid(), 1);
            _editoraRepoMock.Setup(r => r.GetByIdAsync(livro.IdEditora)).ReturnsAsync((Editora)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(livro));
            Assert.Equal("Editora invalida!", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletarLivro_QuandoIdValido()
        {
            // Arrange
            var id = Guid.NewGuid();
            _livroRepoMock.Setup(r => r.DeletarAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.True(result);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveRetornarFalse_QuandoFalhaNaExclusao()
        {
            // Arrange
            var id = Guid.NewGuid();
            _livroRepoMock.Setup(r => r.DeletarAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.False(result);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task BuscarAsync_DeveRetornarListaDeLivros()
        {
            // Arrange
            var livros = new List<Livro> { new Livro("Título", "ISBN", "Autor", "Sinopse", "foto.jpg", 1, Guid.NewGuid(), 1) };
            var userId = Guid.NewGuid();
            var termo = "Título";

            _livroRepoMock.Setup(r => r.BuscarAsync(userId, termo)).ReturnsAsync(livros);

            // Act
            var result = await _service.BuscarAsync(userId, termo);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarLivro_QuandoLivroValido()
        {
            // Arrange
            var usuarioId = Guid.NewGuid();
            var livroId = Guid.NewGuid();
            var editoraId = 1;
            var generoId = 1;

            var livro = new Livro("Novo Título", "Novo ISBN", "Novo Autor", "Nova Sinopse", "novaFoto.jpg", generoId, usuarioId, 1);
            typeof(Livro).GetProperty("Id").SetValue(livro, livroId);

            var livroExistente = new Livro("Antigo Título", "Antigo ISBN", "Antigo Autor", "Antiga Sinopse", "fotoAntiga.jpg", generoId, usuarioId, 1);
            typeof(Livro).GetProperty("Id").SetValue(livroExistente, livroId);

            var editora = new Editora("Editora Atualizada");
            typeof(Editora).GetProperty("Id").SetValue(editora, editoraId);

            _livroRepoMock.Setup(r => r.BucarPorIdAsync(livroId)).ReturnsAsync(livroExistente);
            _editoraRepoMock.Setup(r => r.GetByIdAsync(editoraId)).ReturnsAsync(editora);

            // Act
            var result = await _service.UpdateAsync(livro);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Título", result.Titulo);
            Assert.Equal("Novo ISBN", result.ISBN);
            Assert.Equal("Novo Autor", result.Autor);
            Assert.Equal("Nova Sinopse", result.Sinopse);
            Assert.Equal("novaFoto.jpg", result.FotoPath);
            Assert.Equal(editoraId, result.IdEditora);

            _livroRepoMock.Verify(r => r.AtualizarAsync(livroExistente), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarExcecao_QuandoLivroNaoExiste()
        {
            // Arrange
            var livro = new Livro("Título", "ISBN", "Autor", "Sinopse", "foto.jpg", 1, Guid.NewGuid(), 1);
            typeof(Livro).GetProperty("Id").SetValue(livro, Guid.NewGuid());

            _livroRepoMock.Setup(r => r.BucarPorIdAsync(livro.Id)).ReturnsAsync((Livro)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateAsync(livro));
            Assert.Equal("Livro invalida!", ex.Message);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarExcecao_QuandoEditoraNaoExiste()
        {
            // Arrange
            var livro = new Livro("Título", "ISBN", "Autor", "Sinopse", "foto.jpg", 1, Guid.NewGuid(), 1);
            typeof(Livro).GetProperty("Id").SetValue(livro, Guid.NewGuid());

            _livroRepoMock.Setup(r => r.BucarPorIdAsync(livro.Id)).ReturnsAsync(livro);
            _editoraRepoMock.Setup(r => r.GetByIdAsync(livro.IdEditora)).ReturnsAsync((Editora)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateAsync(livro));
            Assert.Equal("Editora invalida!", ex.Message);
        }
    }
}
