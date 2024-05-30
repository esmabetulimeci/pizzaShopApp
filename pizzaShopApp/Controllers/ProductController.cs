using Application.Common.Interfaces.Redis;
using Application.Operations.Product.Commands;
using Application.Operations.Product.Queries;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Product.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisDbContext;

        public ProductController(IMediator mediator, IRedisDbContext redisDbContext)
        {
            _mediator = mediator;
            _redisDbContext = redisDbContext;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());
            await _redisDbContext.Delete("products");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "products";
            var cacheValue = await _redisDbContext.Get<List<ProductAggregate>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var result = await _mediator.Send(new GetProductQuery());
            await _redisDbContext.Add(cacheKey, result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand(id));
            var cacheKey = $"product_{id}";
            await _redisDbContext.Delete(cacheKey);

     
            await _redisDbContext.Delete("products");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            var cacheKey = $"product_{id}";
            await _redisDbContext.Delete(cacheKey);


            await _redisDbContext.Delete("products");

            return Ok(result);
        }
    }
}
