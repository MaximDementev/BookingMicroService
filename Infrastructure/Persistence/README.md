# PostgreSQL persistence

The Infrastructure project uses EF Core 8 with Npgsql, matching the solution's .NET 8 target.

Register the context in the API composition root:

```csharp
using Infrastructure.Persistence;

builder.Services.AddBookingPersistence(
    builder.Configuration.GetConnectionString("BookingDatabase")
    ?? throw new InvalidOperationException("Connection string 'BookingDatabase' is missing."));
```

Store the connection string in user-secrets or environment variables in deployed environments.
For local development, set `ConnectionStrings:BookingDatabase` to a PostgreSQL connection string.

## Migrations

The design-time factory reads `BOOKING_DATABASE_CONNECTION_STRING`. Set it before running EF tooling.
The fallback connection string is for local development only; change it for your local PostgreSQL credentials.

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialBookingSchema --project Infrastructure --startup-project Infrastructure --output-dir Persistence/Migrations
dotnet ef database update --project Infrastructure --startup-project Infrastructure
```

The context persists bookings and e-tickets, with the associated Passenger and Route relationship graph mapped by EF conventions/configuration.
Do not call `EnsureCreated()` in a migrations-based application.
