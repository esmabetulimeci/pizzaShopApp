﻿using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Product.Commands
{
    public class CreateProductCommand : IRequest<ProductAggregate>
    {
        public CreateProductCommand(string productName, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Name = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Ingredients = ingredients;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand, ProductAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ProductAggregate> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new Exception("PRODUCT_NAME_CANNOT_BE_EMPTY");
                }

                var product = ProductAggregate.Create(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);
                await _dbContext.Products.AddAsync(product, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

    }
}