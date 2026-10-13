using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class ETicketSegmentConfiguration : IEntityTypeConfiguration<ETicketSegment>
{
    public void Configure(EntityTypeBuilder<ETicketSegment> builder)
    {
        builder.ToTable("eticket_segments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SegmentOrder).IsRequired();
        builder.HasIndex("ETicketId", nameof(ETicketSegment.SegmentOrder)).IsUnique();

        builder.Property(x => x.FlightNumber).HasMaxLength(32).IsRequired();
        builder.Property(x => x.AirlineName).HasMaxLength(200).IsRequired();

        builder.Property(x => x.DepartureAirportIataCode).HasMaxLength(3).IsRequired();
        builder.Property(x => x.DepartureAirportName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ScheduledDeparture).IsRequired();

        builder.Property(x => x.ArrivalAirportIataCode).HasMaxLength(3).IsRequired();
        builder.Property(x => x.ArrivalAirportName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ScheduledArrival).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
    }
}