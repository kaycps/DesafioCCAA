using DesafioCCAA.Application.Livro;
using DesafioCCAA.Application.Livro.DTO;
using DesafioCCAA.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCCAA.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroAppService _livroAppService;

        public LivroController(ILivroAppService livroAppService)
        {
            _livroAppService = livroAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] LivroCadastroDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Guid? idUsuario = null;
            var idClaim = User.FindFirst("id")?.Value;

            if (Guid.TryParse(idClaim, out var parsedId))
            {
                idUsuario = parsedId;
            }

            if (idUsuario == null)
            {
                return Unauthorized(new { mensagem = "Ouve um problema com sua autenticação, refaça o login!" });
            }

            var result = await _livroAppService.AddAsync(idUsuario.Value, dto);

            if (result == null)
            {
                return NotFound(new { mensagem = "Não foi possível adicionar um livro ao catalogo!" });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var result = await _livroAppService.Buscar(id);

            if (result == null)
            {
                return NotFound(new { mensagem = "Livro não encontrado." });
            }
            return Ok(result);
        }

        [HttpGet("todos")]
        public async Task<IActionResult> Buscar([FromQuery] string? termo = null)
        {
            Guid? idUsuario = null;
            var idClaim = User.FindFirst("id")?.Value;

            if (Guid.TryParse(idClaim, out var parsedId))
            {
                idUsuario = parsedId;
            }

            if (idUsuario == null)
            {
                return Unauthorized(new { mensagem = "Ouve um problema com sua autenticação, refaça o login!" });
            }

            var resultado = await _livroAppService.BuscarAsync(idUsuario.Value, termo);
            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] LivroEdicaoDTO dto)
        {
            var result = await _livroAppService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var result = await _livroAppService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new { mensagem = "Livro não encontrado." });
            }
            return Ok("Livro deletado com sucesso!");
        }

        

        [HttpGet("relatorio/pdf")]
        public async Task<IActionResult> GerarRelatorioPdf()
        {
            Guid? idUsuario = null;
            var idClaim = User.FindFirst("id")?.Value;

            if (Guid.TryParse(idClaim, out var parsedId))
            {
                idUsuario = parsedId;
            }

            if (idUsuario == null)
            {
                return Unauthorized(new { mensagem = "Ouve um problema com sua autenticação, refaça o login!" });
            }

            var pdfBytes = await _livroAppService.GerarRelatorioLivros(idUsuario.Value); 

            return File(pdfBytes, "application/pdf", "relatorio-livros.pdf");
        }
    }
}
