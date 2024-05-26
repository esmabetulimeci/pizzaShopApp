using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Order.Commands
{
    public class UpdateOrderCommand : IRequest<OrderAggregate>
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } // Müşteri kimliği
        public List<int> ProductIds { get; set; }

        public UpdateOrderCommand(int orderId, int customerId, List<int> productIds)
        {
            OrderId = orderId;
            CustomerId = customerId;
            ProductIds = productIds;
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
                var order = await _dbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == request.OrderId);

                if (order == null)
                {
                    throw new Exception($"Order with ID {request.OrderId} not found.");
                }

                var customer = await _dbContext.Customers.FindAsync(request.CustomerId);
                if (customer == null)
                {
                    throw new Exception($"Customer with ID {request.CustomerId} not found.");
                }

                order.Customer = customer;

                order.Products.Clear();

                foreach (var productId in request.ProductIds)
                {
                    var product = await _dbContext.Products.FindAsync(productId);
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {productId} not found.");
                    }
                    order.Products.Add(product);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return order;
            }
        }
    }
}
