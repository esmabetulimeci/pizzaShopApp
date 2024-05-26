using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPizzaShopAppDbContext
    {
        DbSet<ProductAggregate> Products { get; set; }
        DbSet<OrderAggregate> Orders { get; set; }

        DbSet<CustomerAggregate> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
