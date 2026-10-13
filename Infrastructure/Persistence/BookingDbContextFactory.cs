using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

/// <summary>
/// Enables EF tooling to create the context without starting the API.
/// Set BOOKING_DATABASE_CONNECTION_STRING before running migrations.
/// </summary>
public sealed class BookingDbContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
{
    public BookingDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("BOOKING_DATABASE_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=booking;Username=postgres;Password=postgres";

        var options = new DbContextOptionsBuilder<BookingDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new BookingDbContext(options);
    }
}
