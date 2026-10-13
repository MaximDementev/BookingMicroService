using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class ETicketConfiguration : IEntityTypeConfiguration<ETicket>
{
    public void Configure(EntityTypeBuilder<ETicket> builder)
    {
        builder.ToTable("etickets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TicketNumber)
            .HasMaxLength(32)
            .IsRequired();

        builder.HasIndex(x => x.TicketNumber).IsUnique();

        builder.Property(x => x.ETicketStatus)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(x => x.Amount).HasColumnName("price_amount").HasPrecision(18, 2);
            price.Property(x => x.Currency).HasColumnName("price_currency").HasConversion<string>().HasMaxLength(3);
        });
        builder.Navigation(x => x.Price).IsRequired();

        // Passenger and Route are existing domain relationships; EF maps their related entities too.
        builder.HasOne(x => x.Passenger).WithMany().IsRequired();
        builder.HasMany(x => x.Route).WithMany();
    }
}
