using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class Passenger : Entity
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? MiddleName { get; set; }

        public DateOnly BirthDate { get; set; }

        public Gender Gender { get; set; }

        public required string CitizenshipCode { get; set; }

        public DateTime? DeletedAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<PassengerDocument> Documents { get; set; } = new List<PassengerDocument>();
    }
}
