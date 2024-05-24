using Application.Order.Commands;
using Domain.Model;

namespace pizzaShopApi.Models.Order
{
    public class UpdateOrderRequest
    {

        public int OrderId { get; set; }
        public string UpdatedCustomerName { get; set; }
        public int UpdatedProductId { get; set; }

        public UpdateOrderCommand ToCommand()
        {
            return new UpdateOrderCommand(OrderId, UpdatedCustomerName, UpdatedProductId);
        }

    }

}