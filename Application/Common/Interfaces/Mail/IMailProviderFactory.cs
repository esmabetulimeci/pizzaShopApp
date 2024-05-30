using Application.Operations.Mail.Interfaces;
using Application.Operations.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Mail
{
    public interface IMailProviderFactory
    {
        IMailServiceProvider GetProvider(Settings request);
    }
}
