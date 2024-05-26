using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerAggregate>
    {
        public void Configure(EntityTypeBuilder<CustomerAggregate> builder)
        {
            builder.ToTable("customer");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(c => c.Name).HasColumnName("name").HasColumnType("varchar(250)");
            builder.Property(c => c.Password).HasColumnName("password").HasColumnType("varchar(250)");
            builder.Property(c => c.Email).HasColumnName("email").HasColumnType("varchar(250)");
            builder.Property(c => c.Address).HasColumnName("address").HasColumnType("varchar(250)");
        }
    }
}
