using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserService.V1
{
    /// <summary>
    /// Асинхронный ответ со списком пассажиров и их документами.
    /// </summary>
    public record GetSelectedPassengersResponse(
        Guid CorrelationId,
        List<PassengerDto> Passengers
    );

}
