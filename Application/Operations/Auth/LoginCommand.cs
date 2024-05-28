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
    public class LoginCommand : IRequest<UserAggregate>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public class Handler : IRequestHandler<LoginCommand, UserAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;
            private readonly IJwtService _jwtService;

            public Handler(IPizzaShopAppDbContext dbContext, IJwtService jwtService)
            {
                _dbContext = dbContext;
                _jwtService = jwtService;
            }

            public async Task<UserAggregate> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user == null)
                {
                    throw new Exception("USER_NOT_FOUND");
                }

                // Gelen şifreyi hashle ve veritabanındaki hash ile karşılaştır
                var hashedPassword = HashPassword(request.Password); // Doğrudan HashPassword metodunu kullanıyoruz
                if (user.Password != hashedPassword)
                {
                    throw new Exception("INVALID_PASSWORD");
                }

                return user;
            }

            // Parolanın hashlenmiş halini hesaplayan yardımcı metot
            private string HashPassword(string password)
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }

        }
    }
}
