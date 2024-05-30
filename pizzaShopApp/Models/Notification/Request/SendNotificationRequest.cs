using Application.Operations.Mail.Commands;
using Application.Operations.Mail.Models;

namespace pizzaShopApi.Models.Notification.Request
{
    public class SendNotificationRequest
    {
        public string Subject { get; set; }
        public string Definition { get; set; }
        public Settings Settings { get; set; }
        public SendMailCommand ToCommand()
        {

            return new SendMailCommand(Subject, Definition, Settings);

        }
    }
}
