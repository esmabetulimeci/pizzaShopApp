using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerAggregate>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetCustomerByIdQuery, CustomerAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<CustomerAggregate> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = await _dbContext.Customers
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Products)
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (customer == null)
                {
                    throw new Exception("CUSTOMER_NOT_FOUND");
                }

                return customer;
            }
        }
    }
}
