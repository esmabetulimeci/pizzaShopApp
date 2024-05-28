using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Auth
{
    public class RegisterCommand : IRequest<UserAggregate>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterCommand(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public class Handler : IRequestHandler<RegisterCommand, UserAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;
            private readonly IJwtService _jwtService;

            public Handler(IPizzaShopAppDbContext dbContext, IJwtService jwtService)
            {
                _dbContext = dbContext;
                _jwtService = jwtService;
            }

            public async Task<UserAggregate> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user != null)
                {
                    throw new Exception("USER_ALREADY_EXISTS");
                }

                // Parolanın hashlenmiş halini oluşturucuya göndererek kullanıcı oluştur
                user = new UserAggregate(request.FirstName, request.LastName, request.Email, request.Password); 
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return user;
            }



        }
    }
}
