using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Auth
{
    public interface IAuthService
    {
        public class AccessToken
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }
        public Task<AccessToken> CreateAccessToken(UserAggregate user);


    }
}
