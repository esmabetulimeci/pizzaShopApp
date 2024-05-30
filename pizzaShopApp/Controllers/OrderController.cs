using Application.Common.Interfaces.Redis;
using Application.Operations.Order.Commands;
using Application.Operations.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Order.Request;
using System.Threading.Tasks;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisDbContext;

        public OrderController(IMediator mediator, IRedisDbContext redisDbContext)
        {
            _mediator = mediator;
            _redisDbContext = redisDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = request.ToCommand();
            var order = await _mediator.Send(command);

            await _redisDbContext.Delete("orders");

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var cacheKey = "orders";
            var cacheValue = await _redisDbContext.Get<List<OrderAggregate>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var orders = await _mediator.Send(new GetOrderQuery());
            await _redisDbContext.Add(cacheKey, orders);

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
            var cacheKey = $"order_{order.Id}";
            await _redisDbContext.Delete(cacheKey);

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var command = new DeleteOrderCommand(orderId);
            await _mediator.Send(command);

            var cacheKey = $"order_{orderId}";
            await _redisDbContext.Delete(cacheKey);
            await _redisDbContext.Delete("orders");

            return Ok();
        }
    }
}
