using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressAggregate>
    {
        public int Id { get; set; }

        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetAddressByIdQuery, AddressAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AddressAggregate> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
            {
                var address = await _dbContext.Addresses.FindAsync(request.Id);
                if (address == null)
                {
                    throw new Exception("ADDRESS_NOT_FOUND");
                }
                return address;
            }
        }
    }
   
}
