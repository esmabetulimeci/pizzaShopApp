using Application.Order.Commands;
using Domain.Model;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }

        public AddOrderCommand ToCommand()
        {
            return new AddOrderCommand(CustomerId, ProductIds);
        }
    }
}
