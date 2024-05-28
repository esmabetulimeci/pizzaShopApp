using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.User.Commands
{
    public class UpdateUserCommand : IRequest<UserAggregate>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UpdateUserCommand(int id, string firstName, string lastName, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public class Handler : IRequestHandler<UpdateUserCommand, UserAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<UserAggregate> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {

                var user = await _dbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new Exception("USER_NOT_FOUND");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Password = HashPassword(request.Password);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return user;

            }

            private string HashPassword(string password)
            {
                using (var sha256 = new HMACSHA256())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }
    }
}
