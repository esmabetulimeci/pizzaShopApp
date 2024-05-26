using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderAggregate>
    {
        public void Configure(EntityTypeBuilder<OrderAggregate> builder)
        {
            builder.ToTable("order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(o => o.OrderNumber).HasColumnName("order_number").HasMaxLength(200);
            builder.Property(o => o.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
            builder.Property(o => o.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderDate).HasColumnName("order_date").HasColumnType("date");
            builder.Property(o => o.CustomerId).HasColumnName("customer_id").HasColumnType("int");
           

         
            builder.HasOne(o => o.Customer)
           .WithMany(c => c.Orders)
           .HasForeignKey(o => o.CustomerId)
           .IsRequired() // Müşteri kimliği zorunlu olmalı
           .OnDelete(DeleteBehavior.Cascade);

            // Ürünlerle ilişkiyi konfigure et
            builder.HasMany(o => o.Products)
                   .WithMany(p => p.Orders)
                   .UsingEntity(j => j.ToTable("OrderProduct"));
        }
    }
}
