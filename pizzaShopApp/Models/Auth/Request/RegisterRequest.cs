using Application.Operations.Auth;
using Application.Operations.Auth.Commands;

namespace pizzaShopApi.Models.Auth.Request
{
    public class RegisterRequest
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }


        public RegisterCommand ToCommand()
        {
            return new RegisterCommand(FirstName, LastName, Email, Password);
        }




    }
}
