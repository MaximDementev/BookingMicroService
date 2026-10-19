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

## Data ownership

The Booking Service persists its own `Booking`, `ETicket`, and `ETicketSegment` data.
Entities owned by other microservices (for example, User Service's `Passenger`, or catalog/search services' `Airport`, `Airline`, `Flight`, and `SearchOffer`) are not mapped as EF navigation properties and are not created as tables by this context.

External IDs such as `PassengerId`, `FlightId`, `AirlineId`, and airport IDs are scalar values, not foreign keys. The ticket also stores a small snapshot of passenger name and itinerary details so that historical ticket information does not change when external data changes or becomes unavailable. Do not copy passenger documents or other sensitive data unless the booking workflow explicitly requires it.

## Migrations

The design-time factory reads `BOOKING_DATABASE_CONNECTION_STRING`. Set it before running EF tooling.
The fallback connection string is for local development only; change it for your local PostgreSQL credentials.

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialBookingSchema --project Infrastructure --startup-project Infrastructure --output-dir Persistence/Migrations
dotnet ef database update --project Infrastructure --startup-project Infrastructure
```

The migration creates tables for the Booking Service's local persistence model. It does not create the PostgreSQL database itself.
Do not call `EnsureCreated()` in a migrations-based application.
