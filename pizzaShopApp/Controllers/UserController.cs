using Application.Operations.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.User;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var command = request.ToCommand();
            var user = await _mediator.Send(command);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUserQuery());
            return Ok(users);
        }

        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetUser(int userId)
        //{
        //    var user = await _mediator.Send(new GetUserByIdQuery(userId));
        //    return Ok(user);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        //{
        //    var command = request.ToCommand();
        //    var user = await _mediator.Send(command);

        //    return Ok(user);
        //}

        //[HttpDelete("{userId}")]
        //public async Task<IActionResult> DeleteUser(int userId)
        //{
        //    var command = new DeleteUserCommand(userId);
        //    await _mediator.Send(command);

        //    return Ok();
        //}
    }
}
