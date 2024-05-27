using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductAggregate>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }


        public class Handler : IRequestHandler<GetProductByIdQuery, ProductAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ProductAggregate> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _dbContext.Products.FindAsync(request.Id);
                if (product == null)
                {
                    throw new Exception("PRODUCT_NOT_FOUND");
                }
                return product;
            }
        }




    }
}
