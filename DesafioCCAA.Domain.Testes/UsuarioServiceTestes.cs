using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Domain.Sevices;
using DesafioCCAA.Infrastructure.Repository.Usuario;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Domain.Testes
{
    public class UsuarioServiceTestes
    {
        private readonly Mock<IUnityOfWork> _uowMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly Mock<IResetSenhaRepository> _resetRepoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTestes()
        {
            _uowMock = new Mock<IUnityOfWork>();
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _resetRepoMock = new Mock<IResetSenhaRepository>();
            _tokenServiceMock = new Mock<ITokenService>();

            _uowMock.Setup(x => x.UsuarioRepository).Returns(_usuarioRepoMock.Object);
            _uowMock.Setup(x => x.ResetSenhaRepository).Returns(_resetRepoMock.Object);

            _usuarioService = new UsuarioService(_uowMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Cadastrar_DeveRetornarUsuario_QuandoCadastroForBemSucedido()
        {
            // Arrange
            var novoUsuario = new Usuario("teste", "123456", "prado@gamil.com", DateTime.Now);
            _usuarioRepoMock.Setup(r => r.BuscarPorEmailAsync(novoUsuario.Email))
                            .ReturnsAsync((Usuario)null);

            _usuarioRepoMock.Setup(r => r.CadastrarAsync(It.IsAny<Usuario>()))
                            .ReturnsAsync(novoUsuario);

            // Act
            var resultado = await _usuarioService.Cadastrar(novoUsuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(novoUsuario.Email, resultado.Email);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Cadastrar_DeveLancarExcecao_SeUsuarioJaExistir()
        {
            // Arrange
            var usuarioExistente = new Usuario("teste", "123456", "prado@gamil.com", DateTime.Now);
            _usuarioRepoMock.Setup(r => r.BuscarPorEmailAsync(usuarioExistente.Email))
                            .ReturnsAsync(usuarioExistente);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _usuarioService.Cadastrar(usuarioExistente));
            Assert.Equal("Usuário já cadastrado.", ex.Message);
        }

        [Fact]
        public async Task EditarSenha_DeveAtualizarSenha_QuandoTokenValido()
        {
            // Arrange
            var id = Guid.NewGuid();
            var senhaAntiga = "123456";
            var senhaNova = "novaSenha";

            var usuario = new Usuario("teste", senhaAntiga, "prado@gamil.com", DateTime.Now);
            typeof(Usuario).GetProperty("Id").SetValue(usuario, id);

            var senhaAntigaHash = usuario.HashSenha;

            _usuarioRepoMock.Setup(r => r.BuscarPorIdAsync(id)).ReturnsAsync(usuario);
            _resetRepoMock.Setup(r => r.ObterTokenValidoAsync(id, "1234"))
                          .ReturnsAsync(new ResetSenhaToken { UsuarioId = id, Token = "1234" });

            // Act
            var result = await _usuarioService.EditarSenha(id, "1234", senhaNova);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(senhaAntigaHash, result.HashSenha); 
            Assert.True(BCrypt.Net.BCrypt.Verify(senhaNova, result.HashSenha)); 

            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task EditarSenha_DeveLancarExcecao_QuandoUsuarioInvalido()
        {
            // Arrange
            _usuarioRepoMock.Setup(r => r.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Usuario)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _usuarioService.EditarSenha(Guid.NewGuid(), "1234", "senha"));
        }

        [Fact]
        public async Task EditarSenha_DeveLancarExcecao_QuandoTokenInvalido()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usuario = new Usuario("teste", "123456", "prado@gamil.com", DateTime.Now);
            typeof(Usuario).GetProperty("Id").SetValue(usuario, id);

            _usuarioRepoMock.Setup(r => r.BuscarPorIdAsync(id)).ReturnsAsync(usuario);
            _resetRepoMock.Setup(r => r.ObterTokenValidoAsync(id, "0000"))
                          .ReturnsAsync((ResetSenhaToken)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _usuarioService.EditarSenha(id, "0000", "senha"));
        }
    }
}
