using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Commands
{
    public class AddAddressCommand : IRequest<AddressAggregate>
    {

        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }

        public class Handler : IRequestHandler<AddAddressCommand, AddressAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AddressAggregate> Handle(AddAddressCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FindAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception("USER_NOT_FOUND");
                }
                var address = AddressAggregate.Create(request.AddressTitle, request.Address, user);
                _dbContext.Addresses.Add(address);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return address;
            }
        }
    }
}
