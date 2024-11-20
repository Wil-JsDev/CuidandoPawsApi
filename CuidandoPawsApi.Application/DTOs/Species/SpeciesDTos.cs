using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Species
{
    public class SpeciesDTos
    {
        public int SpeciesId { get; set; }

        public string? DescriptionOfSpecies { get; set; }

        public DateTime? EntryOfSpecies { get; set; } = DateTime.UtcNow;
    }
}
