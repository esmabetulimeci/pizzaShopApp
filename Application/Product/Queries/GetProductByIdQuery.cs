using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Queries
{
    public class GetProductByIdQuery : IRequest<List<ProductAggregate>>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetProductByIdQuery, List<ProductAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<List<ProductAggregate>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (product == null)
                {
                    throw new Exception("NO_PRODUCT_FOUND");
                }
                return new List<ProductAggregate> { product };
            }
        }



    }
}
