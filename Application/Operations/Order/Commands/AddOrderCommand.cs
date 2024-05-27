using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Order.Commands
{
    public class AddOrderCommand : IRequest<OrderAggregate>
    {
        public int UserId { get; set; } // Kullanıcı kimliği
        public int AddressId { get; set; } // Adres kimliği
        public List<int> ProductIds { get; set; }

        public AddOrderCommand(int userId, int addressId, List<int> productIds)
        {
            UserId = userId;
            AddressId = addressId;
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
                var user = await _dbContext.Users
                    .Include(u => u.Orders)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }

                var address = await _dbContext.Addresses
                    .FirstOrDefaultAsync(a => a.Id == request.AddressId && a.UserId == request.UserId, cancellationToken);

                if (address == null)
                {
                    throw new Exception("Adres bulunamadı.");
                }

                var products = await _dbContext.Products
                    .Where(p => request.ProductIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                if (products.Count != request.ProductIds.Count)
                {
                    throw new Exception("Ürünlerden biri bulunamadı.");
                }

                var order = new OrderAggregate
                {
                    OrderNumber = GenerateOrderNumber(),
                    User = user,
                    Address = address,
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
