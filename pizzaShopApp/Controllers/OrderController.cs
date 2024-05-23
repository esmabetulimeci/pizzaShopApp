using Application.Order.Commands;
using Application.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Order;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = request.ToCommand();
            var order = await _mediator.Send(command);

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrderQuery());
            return Ok(orders);
        }



    }
}
