using Application.Operations.Auth.Commands;

namespace pizzaShopApi.Models.Auth.Request
{
    public class LoginUserRequest
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommand ToCommand()
        {
            return new LoginCommand(Email, Password);
        }

    }
}
