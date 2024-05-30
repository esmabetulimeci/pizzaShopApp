using Application.Operations.Address.Commands;

namespace pizzaShopApi.Models.Address.Request
{
    public class UpdateAddressRequest
    {

        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public UpdateAddressRequest(int id, string addressTitle, string address)
        {
            Id = id;
            AddressTitle = addressTitle;
            Address = address;
        }

        public UpdateAddressCommand ToCommand()
        {

            return new UpdateAddressCommand(Id, AddressTitle, Address);

        }


    }
}
