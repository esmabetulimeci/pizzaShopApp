﻿using Application.Operations.Product.Commands;
using Application.Operations.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzaShopApi.Models.Product;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());
            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("id")]
        public async Task<IActionResult> Update (int id, [FromBody] UpdateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand(id));
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(result);
        }
    }
}
