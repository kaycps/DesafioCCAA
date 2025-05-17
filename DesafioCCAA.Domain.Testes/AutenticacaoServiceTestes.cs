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
    public class AutenticacaoServiceTestes
    {
        private readonly Mock<IUnityOfWork> _uowMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AutenticacaoService _service;

        public AutenticacaoServiceTestes()
        {
            _uowMock = new Mock<IUnityOfWork>();
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _tokenServiceMock = new Mock<ITokenService>();

            _uowMock.Setup(u => u.UsuarioRepository).Returns(_usuarioRepoMock.Object);

            _service = new AutenticacaoService(_uowMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Login_DeveRetornarUsuarioComToken_QuandoCredenciaisValidas()
        {
            // Arrange
            var email = "teste@teste.com";
            var senha = "senha123";
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            var usuario = new Usuario("Usuario Teste", senhaHash, email, DateTime.Now);
            var tokenGerado = "token.jwt.mockado";

            _usuarioRepoMock.Setup(r => r.BuscarPorEmailAsync(email)).ReturnsAsync(usuario);
            _tokenServiceMock.Setup(t => t.GerarJWT(usuario)).ReturnsAsync(tokenGerado);

            // Act
            var result = await _service.Login(email, senha);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tokenGerado, result.HashSenha);
        }

        [Fact]
        public async Task Login_DeveLancarExcecao_QuandoEmailNaoEncontrado()
        {
            // Arrange
            var email = "naoexiste@teste.com";
            var senha = "senha123";

            _usuarioRepoMock.Setup(r => r.BuscarPorEmailAsync(email)).ReturnsAsync((Usuario)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.Login(email, senha));

            Assert.Equal("Usuário ou senha inválidos.", ex.Message);
        }

        [Fact]
        public async Task Login_DeveLancarExcecao_QuandoSenhaIncorreta()
        {
            // Arrange
            var email = "teste@teste.com";
            var senhaCorreta = "senha123";
            var senhaErrada = "senhaErrada";
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senhaCorreta);
            var usuario = new Usuario("Usuario Teste", senhaHash, email, DateTime.Now);

            _usuarioRepoMock.Setup(r => r.BuscarPorEmailAsync(email)).ReturnsAsync(usuario);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.Login(email, senhaErrada));

            Assert.Equal("Usuário ou senha inválidos.", ex.Message);
        }
    }
}
