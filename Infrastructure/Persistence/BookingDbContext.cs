using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Infrastructure.Persistence;

/// <summary>
/// EF Core context for the Booking microservice's PostgreSQL database.
/// </summary>
public sealed class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<ETicket> ETickets => Set<ETicket>();
    public DbSet<BookingPassenger> BookingPassengers => Set<BookingPassenger>();
    public DbSet<BookingRoute> BookingRoutes => Set<BookingRoute>();
    public DbSet<BookingFlight> BookingFlights => Set<BookingFlight>();
    public DbSet<BookingItem> BookingItems => Set<BookingItem>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<ReservationPassenger> ReservationPassengers => Set<ReservationPassenger>();
    public DbSet<ETicketSegment> ETicketSegments => Set<ETicketSegment>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BookingModelConfiguration.Configure(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        PrepareChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        PrepareChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void PrepareChanges()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<Domain.Entities.Base.Entity>())
        {
            if (entry.State == EntityState.Added) { entry.Entity.CreatedAt = now; entry.Entity.UpdatedAt = now; }
            if (entry.State == EntityState.Modified) { entry.Entity.UpdatedAt = now; entry.Property(x => x.CreatedAt).IsModified = false; }
            if (entry.Entity is Booking or Reservation)
            {
                if (entry.State == EntityState.Added) entry.Property("Version").CurrentValue = 1L;
                if (entry.State == EntityState.Modified) entry.Property("Version").CurrentValue = checked((long)entry.Property("Version").OriginalValue! + 1L);
            }
        }
    }
}
