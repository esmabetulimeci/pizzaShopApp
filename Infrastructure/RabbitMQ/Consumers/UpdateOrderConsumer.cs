using Application.Common.Interfaces.Mail;
using Application.Operations.Mail.Enums;
using Application.Operations.Mail.Models;
using Domain.Model;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMQ.Consumers
{
    public class UpdateOrderConsumer : IConsumer<UpdateOrderAggregate>
    {
        private readonly IMailProviderFactory _mailProviderFactory;

        public UpdateOrderConsumer(IMailProviderFactory mailProviderFactory)
        {
            _mailProviderFactory = mailProviderFactory;
        }

        public async Task Consume(ConsumeContext<UpdateOrderAggregate> context)
        {
            var order = context.Message;

            var mailProvider = _mailProviderFactory.GetProvider(new Settings
            {
                Provider = Enums.MailServiceProvider.Smtp
            });

            await mailProvider.SendMail(new MailRequest
            {
                Body = $"Siparişiniz güncellenmiştir. Sipariş numarası: {order.Id}",
                Subject = "Sipariş Güncelleme",
                To = order.Email
            });

        }
    }
}
