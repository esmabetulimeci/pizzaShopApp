using Application.Order.Commands;
using Domain.Model;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; } // Müşteri adresi eklendi
        public List<int> ProductIds { get; set; }

        public AddOrderCommand ToCommand()
        {
            return new AddOrderCommand(CustomerName, CustomerAddress, ProductIds); // Müşteri adresi eklenerek komut oluşturuluyor
        }
    }
}
