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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
