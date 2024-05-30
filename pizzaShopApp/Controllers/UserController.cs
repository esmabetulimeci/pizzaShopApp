
using Application.Common.Interfaces.Redis;
using Application.Operations.User.Commands;
using Application.Operations.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Address.Request;
using pizzaShopApi.Models.Address.Response;
using pizzaShopApi.Models.User.Request;
using pizzaShopApi.Models.User.Response;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisDbContext;

        public UserController(IMediator mediator, IRedisDbContext redisDbContext)
        {
            _mediator = mediator;
            _redisDbContext = redisDbContext;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken token)
        {

            var cacheKey = "users";

            var cacheValue = await _redisDbContext.Get<List<GetUserResponse>>(cacheKey);

            if (cacheValue != null)
            {
                return Ok(cacheValue);
            }

            var query = new GetUserQuery();
            var result = await _mediator.Send(query, token);
            var response = result.Select(x=> new GetUserResponse
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = x.CreatedDate,
              Addresses = x.Addresses.Select(a => new GetAddressResponse
              {
                  Id = a.Id,
                  AddressTitle = a.AddressTitle,
                  Address = a.Address,
                  CreatedDate = a.CreatedDate
              }).ToList()
            }).ToList();
            await _redisDbContext.Add(cacheKey, response);

            return Ok(response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id, CancellationToken token)
        {
            var cacheKey = $"user_{id}";

            var cacheValue = await _redisDbContext.Get<GetUserResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, token);

            var response = new GetUserResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                CreatedDate = result.CreatedDate,
                Addresses = result.Addresses.Select(a => new GetAddressResponse
                {
                    Id = a.Id,
                    AddressTitle = a.AddressTitle,
                    Address = a.Address,
                    CreatedDate = a.CreatedDate
                }).ToList()
            };

            await _redisDbContext.Add(cacheKey, response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            var cacheKey = $"user_{id}";
            await _redisDbContext.Delete(cacheKey);

            return Ok("Kullanıcı Başarıyla Güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, DeleteUserRequest request, CancellationToken token)
        {
            var cacheKey = $"user_{id}";

            var command = request.ToCommand();
            await _mediator.Send(command, token);
            await _redisDbContext.Delete(cacheKey);

            return Ok("Kullanıcı Başarıyla Silindi.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{id}/addresses")]
        public async Task<IActionResult> CreateAddress([FromRoute] int id, [FromBody] CreateAddressRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            var cacheKey = $"user_{id}";
            await _redisDbContext.Delete(cacheKey);

            return Ok("Adres Başarıyla Oluşturuldu.");
        }







    }
}
