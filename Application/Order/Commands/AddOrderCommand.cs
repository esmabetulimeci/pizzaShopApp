using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Commands
{
    public class AddOrderCommand : IRequest<OrderAggregate>
    {
        public int CustomerId { get; set; } // Müşteri kimliği
        public List<int> ProductIds { get; set; }

        public AddOrderCommand(int customerId, List<int> productIds)
        {
            CustomerId = customerId;
            ProductIds = productIds;
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
                var customer = await _dbContext.Customers
                    .Include(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

                if (customer == null)
                {
                    throw new Exception("Müşteri bulunamadı.");
                }

                var products = await _dbContext.Products
                    .Where(x => request.ProductIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                if (products.Count != request.ProductIds.Count)
                {
                    throw new Exception("Ürünlerden biri bulunamadı.");
                }

                var order = new OrderAggregate
                {
                    OrderNumber = GenerateOrderNumber(),
                    Customer = customer,
                    Products = products,
                    OrderDate = DateTime.Now
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
