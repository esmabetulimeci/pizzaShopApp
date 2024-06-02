using Application.Operations.Order.Commands;
using Domain.Model;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order.Request
{
    public class CreateOrderRequest
    {

        public int UserId { get; set; }
        public int AddressId { get; set; }
        public List<int> ProductIds { get; set; }

        public string customerName { get; set; }


        public CreateOrderCommand ToCommand()
        {
            return new CreateOrderCommand(UserId, AddressId, ProductIds, customerName);
        }


    }
}
