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
    public class GetProductQuery : IRequest<IEnumerable<OrderAggregate>>
    {
        public class Handler : IRequestHandler<GetProductQuery, IEnumerable<OrderAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<OrderAggregate>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _dbContext.Orders.ToListAsync();
                if (products == null)
                {
                    throw new Exception("NO_PRODUCTS_FOUND");
                }
                return products;
            }
        }


    }
}
