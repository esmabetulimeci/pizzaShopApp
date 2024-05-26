using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customer.Queries
{
    public class GetCustomerQuery : IRequest<IEnumerable<CustomerAggregate>>
    {
        public GetCustomerQuery()
        {

        }


        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, IEnumerable<CustomerAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public GetCustomerQueryHandler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<CustomerAggregate>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {

                var customers = await _dbContext.Customers
                    .Include(c => c.Orders)
                    .ToListAsync();

                return customers;
            }
        }



    }
}
