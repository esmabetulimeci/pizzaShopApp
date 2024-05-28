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
    public class UpdateAddressCommand : IRequest<AddressAggregate>
    {
        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public UpdateAddressCommand(int id, string addressTitle, string address)
        {
            Id = id;
            AddressTitle = addressTitle;
            Address = address;
        }

        public class Handler : IRequestHandler<UpdateAddressCommand, AddressAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AddressAggregate> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _dbContext.Addresses.FindAsync(request.Id);
                if (address == null)
                {
                    throw new Exception("ADDRESS_NOT_FOUND");
                }
                address.Update(request.AddressTitle, request.Address);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return address;
            }


        }
    }
  
}
