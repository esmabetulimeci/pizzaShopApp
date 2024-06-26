﻿using Application.Operations.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Mail.Interfaces
{
    public interface IMailServiceProvider
    {
        Task Send(Settings settings, string messageString, string titleString, string address);
    }
}
