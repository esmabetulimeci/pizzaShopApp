using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.User.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {

        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteUserCommand, int>
        {
            private IMediator _mediator;
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new Exception("USER_NOT_FOUND");
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return user.Id;
            }
        }







    }
}
