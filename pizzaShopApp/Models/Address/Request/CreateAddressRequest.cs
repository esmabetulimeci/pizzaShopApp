using Application.Operations.Address.Commands;

namespace pizzaShopApi.Models.Address.Request
{
    public class CreateAddressRequest
    {
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public CreateAddressCommand ToCommand(int userId)
        {
            return new CreateAddressCommand(AddressTitle, Address, userId);
        }



    }

}
