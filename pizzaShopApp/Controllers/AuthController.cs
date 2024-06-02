using Application.Common.Interfaces;
using Application.Common.Interfaces.Redis;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Auth.Request;
using System.IdentityModel.Tokens.Jwt;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public AuthController(IMediator mediator, IRedisDbContext redisClient = null)
        {
            _mediator = mediator;
            _redisClient = redisClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, token);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] pizzaShopApi.Models.Auth.Request.RegisterRequest request, CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, token);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            return Ok("Çıkış işlemi başarılı.");
        }
    }
}