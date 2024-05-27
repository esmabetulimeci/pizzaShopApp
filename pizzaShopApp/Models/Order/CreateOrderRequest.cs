using Application.Operations.Order.Commands;
using Domain.Model;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order
{
    public class CreateOrderRequest
    {

        public int UserId { get; set; }
        public int AddressId { get; set; }
        public List<int> ProductIds { get; set; }


        public AddOrderCommand ToCommand()
        {
            return new AddOrderCommand(UserId, AddressId, ProductIds);
        }


    }
}
