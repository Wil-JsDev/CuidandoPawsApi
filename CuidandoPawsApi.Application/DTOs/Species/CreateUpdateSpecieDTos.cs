using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Species
{
    public class CreateUpdateSpecieDTos
    {
        public string? DescriptionOfSpecies { get; set; }

        public DateTime? EntryOfSpecies { get; set; } = DateTime.UtcNow;
    }
}
