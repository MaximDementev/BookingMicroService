using Domain.Entities.Base;
using Domain.Enums;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class PassengerDocument : Entity
    {
        public DocumentType DocumentType { get; set; }

        public string? SeriesEncrypted { get; set; }

        public required string NumberEncrypted { get; set; }

        public required string CountryCode { get; set; }

        public DateOnly? ExpiresAt { get; set; }

        public Passenger Passenger { get; set; } = null!;
    }
}