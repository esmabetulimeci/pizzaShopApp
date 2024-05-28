using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.User.Queries
{
    public class GetUserByIdQuery : IRequest<UserAggregate>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetUserByIdQuery, UserAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<UserAggregate> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new Exception("USER_NOT_FOUND");
                }
                return user;
            }
        }
    }
}
