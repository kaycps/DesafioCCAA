using DesafioCCAA.Application.Login;
using DesafioCCAA.Application.Login.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCCAA.API.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IAutenticacaoAppService _autenticacaoAppService;

        public LoginController(IAutenticacaoAppService appService)
        {
            _autenticacaoAppService = appService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AutenticacaoDTO dto)
        {
            var token = await _autenticacaoAppService.Login(dto);

            return Ok(token);
        }
    }
}
