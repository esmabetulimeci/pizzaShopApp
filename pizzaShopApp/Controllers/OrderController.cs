using Application.Operations.Order.Commands;
using Application.Operations.Order.Queries;
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

        [HttpGet("{orderId}")]

        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId));
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            var command = request.ToCommand();
            var order = await _mediator.Send(command);

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var command = new DeleteOrderCommand(orderId);
            await _mediator.Send(command);

            return Ok();
        }





    }
}
