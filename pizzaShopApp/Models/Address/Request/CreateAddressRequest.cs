using Application.Operations.Address.Commands;

namespace pizzaShopApi.Models.Address.Request
{
    public class CreateAddressRequest
    {
        public int UserId { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }

       


        public CreateAddressCommand ToCommand()
        {
            return new CreateAddressCommand(AddressTitle, Address, UserId);
        }



    }



    

}
