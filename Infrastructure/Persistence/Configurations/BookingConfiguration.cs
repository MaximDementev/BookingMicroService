using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookingCode)
            .HasMaxLength(32)
            .IsRequired();

        builder.HasIndex(x => x.BookingCode).IsUnique();

        builder.Property(x => x.BookingStatus)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.BookingDate).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.OwnsOne(x => x.TotalPrice, price =>
        {
            price.Property(x => x.Amount).HasColumnName("total_amount").HasPrecision(18, 2);
            price.Property(x => x.Currency).HasColumnName("total_currency").HasConversion<string>().HasMaxLength(3);
        });
        builder.Navigation(x => x.TotalPrice).IsRequired();

        builder.HasMany(x => x.ETickets)
            .WithOne()
            .HasForeignKey("BookingId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
