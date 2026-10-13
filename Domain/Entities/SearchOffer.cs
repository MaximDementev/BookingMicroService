using Domain.Entities.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SearchOffer : Entity
    {
        public required Airline Airline { get; set; }

        public string ProviderOfferId { get; set; } = string.Empty;

        public required Route Route { get; set; }

        public int PassengersCount { get; set; }

        public required Price TotalPrice { get; set; }

        public Currency Currency { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
