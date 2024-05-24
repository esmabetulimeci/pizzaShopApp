using Application.Order.Commands;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public string UpdatedCustomerName { get; set; }
        public string UpdatedCustomerAddress { get; set; }
        public List<int> UpdatedProductIds { get; set; } 

        public UpdateOrderCommand ToCommand()
        {
            return new UpdateOrderCommand(OrderId, UpdatedCustomerName, UpdatedCustomerAddress, UpdatedProductIds);
        }
    }
}
