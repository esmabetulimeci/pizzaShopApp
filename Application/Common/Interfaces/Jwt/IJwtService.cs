using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserAggregate user);

    }
}
