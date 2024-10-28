using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        public string? Treatment { get; set; }

        public string? Recommendations { get; set; }

        public int IdPet { get; set; }

        public Pets? Pet { get; set; }
    }
}
