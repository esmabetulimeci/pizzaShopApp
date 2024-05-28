using Application.Operations.Address.Commands;
using Application.Operations.Address.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Address;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] CreateAddressRequest request)
        {
            var command = request.ToCommand();
            var address = await _mediator.Send(command);
            return Ok(address);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var query = new GetAddressQuery();
            var addresses = await _mediator.Send(query);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var query = new GetAddressByIdQuery(id);
            var address = await _mediator.Send(query);
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressRequest request)
        {
            var command = request.ToCommand();
            var address = await _mediator.Send(command);
            return Ok(address);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var command = new DeleteAddressCommand(id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
