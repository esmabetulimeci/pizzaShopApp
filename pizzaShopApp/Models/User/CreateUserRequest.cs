using Application.Operations.User.Commands;

namespace pizzaShopApi.Models.User
{
    public class CreateUserRequest
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }


        public AddUserCommand ToCommand()
        {

            return new AddUserCommand(firstName, lastName, email);

        }

    }
}
