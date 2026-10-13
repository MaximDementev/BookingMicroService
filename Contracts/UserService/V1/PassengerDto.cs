using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserService.V1
{
    public record PassengerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    GenderDto Gender,
    string CitizenshipCode,
    List<PassengerDocumentDto> Documents
);

}
