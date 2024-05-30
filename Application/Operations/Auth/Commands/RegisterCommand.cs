using Application.Common.Interfaces;
using Application.Common.Interfaces.Jwt;
using Application.Common.Interfaces.Redis;
using Application.Operations.Auth.Validators;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Auth.Commands
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
            private readonly IRedisDbContext _redisDbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IJwtService jwtService, IRedisDbContext redisDbContext)
            {
                _dbContext = dbContext;
                _jwtService = jwtService;
                _redisDbContext = redisDbContext;
            }

            public async Task<UserAggregate> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.IsValid)
                {
                    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                    if (user != null)
                    {
                        throw new Exception("USER_ALREADY_EXISTS");
                    }
                    var newUser = new UserAggregate(request.FirstName, request.LastName, request.Email, request.Password);
                    _dbContext.Users.Add(newUser);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    var token = _jwtService.GenerateJwtToken(newUser);
                    await _redisDbContext.Add(token, newUser);
                    return newUser;
                }
                else
                {
                    throw new Exception("INVALID_DATA");
                }





            }
        }
    }
}
