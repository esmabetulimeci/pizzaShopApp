using Application.Common.Interfaces.Redis;
using Application.Operations.Address.Commands;
using Application.Operations.Address.Queries;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Address.Request;
using System.Threading.Tasks;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisDbContext;

        public AddressController(IMediator mediator, IRedisDbContext redisDbContext)
        {
            _mediator = mediator;
            _redisDbContext = redisDbContext;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
        {
            var command = request.ToCommand();
            var address = await _mediator.Send(command);

            await _redisDbContext.Delete("addresses");

            return Ok(address);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAddresses(string searchKeyword) // Burada searchKeyword parametresi ekleniyor
        {
            var cacheKey = "addresses";
            var cacheValue = await _redisDbContext.Get<List<AddressAggregate>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var addresses = await _mediator.Send(new GetAddressQuery { SearchKeyword = searchKeyword }); // SearchKeyword parametresi istemciden alınan metinle dolduruluyor
            await _redisDbContext.Add(cacheKey, addresses);

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await _mediator.Send(new GetAddressByIdQuery(id));
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressRequest request)
        {
            var command = request.ToCommand();
            var address = await _mediator.Send(command);

            var cacheKey = $"address_{id}";
            await _redisDbContext.Delete(cacheKey);

            return Ok(address);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var command = new DeleteAddressCommand(id);
            await _mediator.Send(command);

            var cacheKey = $"address_{id}";
            await _redisDbContext.Delete(cacheKey);
            await _redisDbContext.Delete("addresses");

            return Ok();
        }
    }
}
