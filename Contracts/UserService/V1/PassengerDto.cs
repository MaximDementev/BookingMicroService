using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Contracts.UserService.V1
{
    public record PassengerDto(
    long Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    GenderDto Gender,
    string CitizenshipCode,
    List<PassengerDocumentDto> Documents
);

}
