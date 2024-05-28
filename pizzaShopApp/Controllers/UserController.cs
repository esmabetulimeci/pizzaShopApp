
using Application.Operations.User.Commands;
using Application.Operations.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUserQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var updatedUser = await _mediator.Send(command);
            return Ok(updatedUser);
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUserId = await _mediator.Send(new DeleteUserCommand(id));
            return Ok(deletedUserId);
        }






    }
}
