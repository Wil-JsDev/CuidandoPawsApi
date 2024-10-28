using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Models
{
    public class Pets
    {
        public int Id { get; set; }

        public string? NamePaws { get; set; }

        public string? Bred { get; set; }

        public int Age { get; set; }

        public string? Color { get; set; }

        public bool AdoptionStatus { get; set; } = true;

        public string? Notes { get; set; }

        public DateTime DateOfEntry { get; set; } = DateTime.UtcNow;

        public string? Gender { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public MedicalRecord? MedicalRecord { get; set; }

        public Adoption? Adoption { get; set; }

        public int SpeciesId { get; set; }

        public Species? Species { get; set; }

    }
}
