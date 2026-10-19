using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddBookingPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<BookingDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
