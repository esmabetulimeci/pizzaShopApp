using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.User.Commands
{
    public class AddUserCommand : IRequest<UserAggregate>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public AddUserCommand(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public class Handler : IRequestHandler<AddUserCommand, UserAggregate>
        {
            private IMediator _mediator;
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<UserAggregate> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                var user = UserAggregate.Create(request.FirstName, request.LastName, request.Email);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return user;
            }


        }
    }
}
