using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: Entity
    {
        public required string Email { get; set; }
        public string? Phone { get; set; } 

        public UserStatus Status { get; set; } //что это?

        public DateTime? EmailVerifiedAt { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }

        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

        public UserCredential Credential { get; set; } = null!;
        public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
        public ICollection<UserPushDevice> PushDevices { get; set; } = new List<UserPushDevice>();
        public ICollection<NotificationPreference> NotificationPreferences { get; set; } = new List<NotificationPreference>();
    }
}
