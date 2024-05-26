using Application.Order.Commands;
using System.Collections.Generic;

namespace pizzaShopApi.Models.Order
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public int UpdatedCustomerId { get; set; } 
        public List<int> UpdatedProductIds { get; set; }

        public UpdateOrderCommand ToCommand()
        {
            return new UpdateOrderCommand(OrderId, UpdatedCustomerId, UpdatedProductIds);
        }
    }
}
