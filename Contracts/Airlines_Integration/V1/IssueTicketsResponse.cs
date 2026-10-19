using System.Collections.Generic;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Contracts.Airlines_Integration.V1;

public enum TicketIssuanceStatus
{
    Issued,
    PartiallyIssued,
    Pending,
    Rejected,
    Unknown
}

public sealed record IssuedTicketDto(
    string TicketNumber,
    long BookingPassengerId,
    IReadOnlyList<string> SegmentIds);

/// <summary>
/// Result of a ticket issuance attempt. Pending and Unknown require status reconciliation.
/// </summary>
public sealed record IssueTicketsResponse(
    TicketIssuanceStatus Status,
    IReadOnlyList<IssuedTicketDto> Tickets,
    string? ErrorCode);
