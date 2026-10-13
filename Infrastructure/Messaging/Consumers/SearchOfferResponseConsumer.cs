using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.Consumers
{
    public class SearchOfferResponseConsumer : IConsumer<SearchOfferResponseEvent>
    {
        private readonly IRepository<SearchOffer> _repository;

        public async Task Consume(ConsumeContext<SearchOfferResponseEvent> context)
        {
            var eventData = context.Message;

            foreach (var dto in eventData.Offers)
            {
                // Маппинг DTO во внутреннюю доменную сущность
                var domainOffer = new SearchOffer
                {
                    ProviderOfferId = dto.ProviderOfferId,
                    PassengersCount = dto.PassengersCount,
                    Currency = dto.Currency,
                    ExpiresAt = dto.ExpiresAt,

                    // Создаем доменные объекты из DTO данных
                    Airline = new Airline { Code = dto.Airline.Code, Name = dto.Airline.Name },
                    Route = new Route { Origin = dto.Route.Origin, Destination = dto.Route.Destination },
                    TotalPrice = new Price { Amount = dto.TotalPrice.Amount, Currency = dto.TotalPrice.Currency }
                };

                await _repository.AddAsync(domainOffer);
            }
        }
    }

}
