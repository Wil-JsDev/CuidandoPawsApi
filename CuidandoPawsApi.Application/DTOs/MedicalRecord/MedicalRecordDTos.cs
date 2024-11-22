using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.MedicalRecord
{
    public class MedicalRecordDTos
    {
        public int MedicalRecordId { get; set; }

        public string? Treatment { get; set; }

        public string? Recommendations { get; set; }

        public int IdPet { get; set; }
    }
}
