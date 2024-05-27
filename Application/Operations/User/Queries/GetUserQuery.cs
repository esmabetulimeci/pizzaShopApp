using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.User.Queries
{
    public class GetUserQuery : IRequest<IEnumerable<UserAggregate>>
    {

        public class Handler : IRequestHandler<GetUserQuery, IEnumerable<UserAggregate>>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<UserAggregate>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _dbContext.Users.ToListAsync();
                if (users == null)
                {
                    throw new Exception("NO_USERS_FOUND");
                }
                return users;
            }
        }





    }
}
