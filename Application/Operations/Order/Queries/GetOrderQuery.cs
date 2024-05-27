using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Order.Queries
{
    public class GetOrderQuery : IRequest<IEnumerable<OrderAggregate>>
    {

        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IEnumerable<OrderAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public GetOrderQueryHandler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<OrderAggregate>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {

                var orders = await _dbContext.Orders
                    .Include(o => o.Products)
                    .ToListAsync();

                return orders;
            }
        }



    }
}
