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
        builder.HasIndex(x => x.PassengerId);

        builder.Property(x => x.PassengerId).IsRequired();
        builder.Property(x => x.PassengerFirstName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.PassengerLastName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.PassengerMiddleName).HasMaxLength(150);

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

        // Segments are owned by this ticket and stored locally; external IDs inside them are scalar values.
        builder.HasMany(x => x.Segments)
            .WithOne()
            .HasForeignKey("ETicketId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}