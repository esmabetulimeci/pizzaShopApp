﻿using Application.Operations.Mail.Interfaces;
using Application.Operations.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.MailProviders
{
    public class Setrow : IMailServiceProvider
    {
        public Task Send(Settings settings, string messageString, string titleString)
        {
            throw new NotImplementedException();
        }

        public Task Send(Settings settings, string messageString, string titleString, string address)
        {
            throw new NotImplementedException();
        }
    }
}
