using Application.Operations.User.Commands;

namespace pizzaShopApi.Models.User
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public UpdateUserCommand ToCommand(int id)
        {
            return new UpdateUserCommand(id, FirstName, LastName, Email, Password);
        }
    }
}
