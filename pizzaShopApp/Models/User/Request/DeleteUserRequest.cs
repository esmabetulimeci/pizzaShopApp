using Application.Operations.User.Commands;
using DocumentFormat.OpenXml.Spreadsheet;

namespace pizzaShopApi.Models.User.Request
{
    public class DeleteUserRequest
    {
        public int Id { get; set; }

        public DeleteUserCommand ToCommand()
        {
            return new DeleteUserCommand(Id);

        }

    }
}
