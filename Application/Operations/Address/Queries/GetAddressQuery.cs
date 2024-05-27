using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Queries
{
    public class GetAddressQuery : IRequest<IEnumerable<AddressAggregate>>
    {
        public class Handler : IRequestHandler<GetAddressQuery, IEnumerable<AddressAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<AddressAggregate>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                var addresses = await _dbContext.Addresses.ToListAsync();
                if (addresses == null)
                {
                    throw new Exception("NO_ADDRESSES_FOUND");
                }
                return addresses;
            }
        }

    }
    
}
