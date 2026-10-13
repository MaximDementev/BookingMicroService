using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserService.V1
{
    public record PassengerDocumentDto(
    Guid Id,
    DocumentTypeDto DocumentType,
    string? SeriesEncrypted,    // Передаем зашифрованные данные, если UserService отвечает за их безопасность
    string NumberEncrypted,
    string CountryCode,
    DateOnly? ExpiresAt
);

}
