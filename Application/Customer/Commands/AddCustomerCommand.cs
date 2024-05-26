using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customer.Commands
{
    public class AddCustomerCommand:IRequest<CustomerAggregate>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public AddCustomerCommand(string name, string password, string email, string address)
        {
            Name = name;
            Password = password;
            Email = email;
            Address = address;
        }

        public class Handler : IRequestHandler<AddCustomerCommand, CustomerAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<CustomerAggregate> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new CustomerAggregate
                {
                    Name = request.Name,
                    Password = request.Password,
                    Email = request.Email,
                    Address = request.Address
                };

                _dbContext.Customers.Add(customer);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return customer;
            }
        }
    }
}
