using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Commands
{
    public class DeleteAddressCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteAddressCommand(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<DeleteAddressCommand, int>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<int> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _dbContext.Addresses.FindAsync(request.Id);
                if (address == null)
                {
                    throw new Exception("ADDRESS_NOT_FOUND");
                }
                _dbContext.Addresses.Remove(address);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return address.Id;
            }
        }
    }
}
