using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Commands
{
    public class AddOrderCommand : IRequest<OrderAggregate>
    {
        public string CustomerName { get; set; }
        public int ProductId { get; set; }

        public AddOrderCommand(string customerName, int productId)
        {
            CustomerName = customerName;
            ProductId = productId;
        }

        public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderAggregate>
        {
            private readonly IPizzaShopAppDbContext _dbContext;

            public AddOrderCommandHandler(IPizzaShopAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<OrderAggregate> Handle(AddOrderCommand request, CancellationToken cancellationToken)
            {
             
                var product = await _dbContext.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                   
                    throw new Exception("Product not found");
                }

                
                double totalAmount = product.Price;

              
                string orderNumber = GenerateOrderNumber();

             
                var order = new OrderAggregate
                {
                    CustomerName = request.CustomerName,
                    TotalAmount = totalAmount,
                    DiscountAmount = 0, 
                    OrderDate = DateTime.Now, 
                    OrderNumber = orderNumber, 
                    Products = new List<ProductAggregate> { product } 
                };

              
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return order;
            }

         
            private string GenerateOrderNumber()
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                return new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
