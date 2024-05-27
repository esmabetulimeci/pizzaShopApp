
using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Order.Commands
{
    public class UpdateOrderCommand : IRequest<OrderAggregate>
    {
        public int OrderId { get; set; }
        public int UserId { get; set; } // Kullanıcı kimliği
        public List<int> ProductIds { get; set; }

        public UpdateOrderCommand(int orderId, int userId, List<int> productIds)
        {
            OrderId = orderId;
            UserId = userId;
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
                    throw new Exception($"Sipariş ID'si {request.OrderId} ile bulunamadı.");
                }

                var user = await _dbContext.Users.FindAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception($"Kullanıcı ID'si {request.UserId} ile bulunamadı.");
                }

                order.User = user;

                order.Products.Clear();

                foreach (var productId in request.ProductIds)
                {
                    var product = await _dbContext.Products.FindAsync(productId);
                    if (product == null)
                    {
                        throw new Exception($"Ürün ID'si {productId} ile bulunamadı.");
                    }
                    order.Products.Add(product);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return order;
            }
        }
    }
}
