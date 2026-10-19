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
    public record PassengerDocumentDto(
    long Id,
    DocumentTypeDto DocumentType,
    string? SeriesEncrypted,    // Передаем зашифрованные данные, если UserService отвечает за их безопасность
    string NumberEncrypted,
    string CountryCode,
    DateOnly? ExpiresAt
);

}
