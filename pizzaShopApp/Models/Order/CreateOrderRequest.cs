using Application.Order.Commands;
using Domain.Model;

namespace pizzaShopApi.Models.Order
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }

        public AddOrderCommand ToCommand()
        {

            return new AddOrderCommand(CustomerName, ProductId);
        }
    }



}
