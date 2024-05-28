using Application.Common.Interfaces;
using Application.Operations.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;

        public AuthController(IJwtService jwtService, IMediator mediator)
        {
            _jwtService = jwtService;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kaydınız tamamlandı");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var user = await _mediator.Send(command);
            var token = _jwtService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
