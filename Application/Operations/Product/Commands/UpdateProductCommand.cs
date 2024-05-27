using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Product.Commands
{
    public class UpdateProductCommand : IRequest<ProductAggregate>
    {
        public UpdateProductCommand(int id, string productName, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Id = id;
            Name = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Ingredients = ingredients;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand, ProductAggregate>
        {
            private IMediator _mediator;
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<ProductAggregate> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new Exception("PRODUCT_NAME_CANNOT_BE_EMPTY");
                }

                var product = await _dbContext.Products.FindAsync(request.Id);
                if (product == null)
                {
                    throw new Exception("PRODUCT_NOT_FOUND");
                }

                product.Update(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

    }
}
