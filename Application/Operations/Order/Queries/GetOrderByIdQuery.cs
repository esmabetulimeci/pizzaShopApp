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
    public class GetOrderByIdQuery : IRequest<OrderAggregate>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetOrderByIdQuery, OrderAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<OrderAggregate> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Products)
                    .FirstOrDefaultAsync(o => o.Id == request.Id);

                if (order == null)
                {
                    throw new Exception("ORDER_NOT_FOUND");
                }

                return order;
            }

        }
    }
}
