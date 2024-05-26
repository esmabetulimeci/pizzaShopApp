using Application.Customer.Commands;

namespace pizzaShopApi.Models.Customer
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public AddCustomerCommand ToCommand()
        {
            return new AddCustomerCommand(Name, Password, Email, Address);
        }


    }
}
