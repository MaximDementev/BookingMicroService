namespace Contracts.Airlines_Integration.V1;

/// <summary>
/// Requests a fresh validation of a previously returned supplier offer.
/// </summary>
public sealed record ValidateOfferRequest(
    long OfferId,
    string SupplierId,
    string ProviderOfferId,
    int PassengersCount);
