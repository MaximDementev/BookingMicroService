using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Infrastructure.Persistence;

/// <summary>
/// Enables EF tooling to create the context without starting the API.
/// Set BOOKING_DATABASE_CONNECTION_STRING before running migrations.
/// </summary>
public sealed class BookingDbContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
{
    public BookingDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("BOOKING_DATABASE_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Set BOOKING_DATABASE_CONNECTION_STRING; database credentials are never stored in source code.");

        var options = new DbContextOptionsBuilder<BookingDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new BookingDbContext(options);
    }
}
