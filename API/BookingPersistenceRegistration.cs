using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt

namespace API;

/// <summary>Composition hook for the future HTTP host. Does not create or migrate databases at startup.</summary>
public static class BookingPersistenceRegistration
{
    public static IServiceCollection AddBookingDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BookingDatabase")
            ?? Environment.GetEnvironmentVariable("BOOKING_DATABASE_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Configure ConnectionStrings:BookingDatabase or BOOKING_DATABASE_CONNECTION_STRING.");
        return services.AddBookingPersistence(connectionString);
    }
}
