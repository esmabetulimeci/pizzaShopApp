using Application.Common.Interfaces.Mail;
using Application.Operations.Mail.Interfaces;
using Application.Operations.Mail.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Operations.Mail.Enums.Enums;

namespace Infrastructure.Repositories.MailProviders
{
    public class MailProviderFactory : IMailProviderFactory
    {
        private readonly IConfiguration _configuration;
        public IMailServiceProvider GetProvider(Settings request)
        {
            IMailServiceProvider provider = null;

            switch (request.Provider)
            {
                case MailServiceProvider.Smtp:
                    provider = new Smtp(_configuration);
                    break;
                default:
                    break;
            }
            return provider;


        }
    }
}
