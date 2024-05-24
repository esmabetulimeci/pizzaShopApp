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
    public class UpdateOrderCommand : IRequest<OrderAggregate>
    {
        public int OrderId { get; set; }
        public string UpdatedCustomerName { get; set; }
        public int UpdatedProductId { get; set; }

        public UpdateOrderCommand(int orderId, string updatedCustomerName, int updatedProductId)
        {
            OrderId = orderId;
            UpdatedCustomerName = updatedCustomerName;
            UpdatedProductId = updatedProductId;
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderAggregate>
    {
        private readonly IPizzaShopAppDbContext _dbContext;

        public UpdateOrderCommandHandler(IPizzaShopAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderAggregate> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(request.OrderId);
            if (order == null)
            {
                throw new Exception("ORDER_NOT_FOUND");
            }

            var product = await _dbContext.Products.FindAsync(request.UpdatedProductId);
            if (product == null)
            {
                throw new Exception("PRODUCT_NOT_FOUND");
            }

            order.Update(request.UpdatedCustomerName, new List<ProductAggregate> { product });
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }


    }

}