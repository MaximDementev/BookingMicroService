using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Domain.Entities.Base
{
    public abstract class Entity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
