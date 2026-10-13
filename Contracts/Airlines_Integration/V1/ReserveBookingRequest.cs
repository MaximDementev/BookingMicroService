using System;
using System.Collections.Generic;

namespace Contracts.Airlines_Integration.V1;

public sealed record PassportDto(
    string Number,
    string IssuingCountry,
    DateOnly? ExpiryDate);

public sealed record ReservePassengerDto(
    long BookingPassengerId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth,
    string? Gender,
    string Nationality,
    PassportDto Passport,
    string? Email,
    string? Phone);

/// <summary>
/// Requests a supplier reservation after passenger details have been collected.
/// </summary>
public sealed record ReserveBookingRequest(
    long BookingId,
    string IdempotencyKey,
    long OfferId,
    string SupplierId,
    string ProviderOfferId,
    MoneyDto ExpectedPrice,
    IReadOnlyList<ReservePassengerDto> Passengers);
