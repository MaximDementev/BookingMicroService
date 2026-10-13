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

Create and apply the initial migration from the solution directory:

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialBookingSchema --project Infrastructure --startup-project API --output-dir Persistence/Migrations
dotnet ef database update --project Infrastructure --startup-project API
```

The API startup project must configure the same connection string and reference Infrastructure.
Do not call `EnsureCreated()` in a migrations-based application.
