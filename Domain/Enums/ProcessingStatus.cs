// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt

namespace Domain.Enums;

public enum BookingStatus { Draft, Pending, Paid, Completed, PartiallyCompleted, Expired, Cancelled, Refunded, RequiresReview }
public enum PaymentStatus { NotStarted, Pending, Succeeded, Failed, RefundPending, PartiallyRefunded, Refunded, Unknown }
public enum IssuanceStatus { NotStarted, Pending, Issued, PartiallyIssued, Rejected, Unknown }
public enum ReservationStatus { Pending, Confirmed, Rejected, Expired, Cancelled, Unknown }
