using DesafioCCAA.Application.Usuario;
using DesafioCCAA.Application.Usuario.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCCAA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioAppService usuarioAppService)
        {
            _logger = logger;
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Create( UsuarioCadastroDTO usuario)
        {
            if(usuario == null)
            {
                return BadRequest();
            }

            var result = await _usuarioAppService.Cadastrar(usuario);

            if(result == null)
            {
                return NotFound(new { mensagem = "Não foi possível criar o usuário. Verifique os dados informados." });
            }

            return Ok(result);
        }

        [HttpPost("solicitar-reset-senha")]
        public async Task<IActionResult> SolicitarResetSenha([FromBody] string email)
        {
            
            var result = await _usuarioAppService.CriarTokenResetSenhaAsync(email);
            

            return Ok(result);
        }

        [HttpPut("resetar-senha")]
        public async Task<IActionResult> ResetarSenha([FromBody] UsuarioResetSenhaDTO dto)
        {           

            var sucesso = await _usuarioAppService.EditarSenha(dto);
            if (sucesso == null)
                return NotFound("Não foi possivel alterar a senha, verifique os dados informados!");

            return Ok(sucesso);
        }

    }
}
