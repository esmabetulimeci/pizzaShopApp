using Application.Operations.Address.Commands;

namespace pizzaShopApi.Models.Address
{
    public class CreateAddressRequest
    {

        public string addressTitle { get; set; }
        public string address { get; set; }
        public int userId { get; set; }

        public AddAddressCommand ToCommand()
        {

            return new AddAddressCommand(addressTitle, address, userId);

        }


    }
}
