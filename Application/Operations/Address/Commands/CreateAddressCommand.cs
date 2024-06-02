using Application.Common.Interfaces;
using Application.Common.Interfaces.Redis;
using Application.Operations.Address.Validators;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Commands
{
    public class CreateAddressCommand : IRequest<AddressAggregate>
    {
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }

        public CreateAddressCommand(string addressTitle, string address, int userId)
        {
            AddressTitle = addressTitle;
            Address = address;
            UserId = userId;
        }

        public class Handler : IRequestHandler<CreateAddressCommand, AddressAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;
         

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            
            }

            public async Task<AddressAggregate> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user is null)
                {
                    throw new Exception("Kullanıcı bulunamadı.");

                }

                var validator = new CreateAddressValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    throw new Exception("Geçersiz veri");
                }



                var address= AddressAggregate.Create(request.AddressTitle, request.Address, user);

                await _dbContext.Addresses.AddAsync(address, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return address;

            }
        }
    }
}

