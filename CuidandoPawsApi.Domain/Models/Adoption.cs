using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Models
{
    public class Adoption
    {
        public  int Id { get; set; }

        public DateTime AdoptionDate { get; set; } = DateTime.UtcNow;

        public bool Status { get; set; } = true;

        public string? Notes { get; set; }

        public int IdPets { get; set; }

        public Pets? Pets { get; set; }
    }
}
