using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Pets
{
    public class UpdatePetsDTos
    {
        public string? NamePaws {  get; set; }

        public string? Bred { get; set; }

        public  int Age { get; set; }

        public string? Gender { get; set; }

        public int SpeciesId { get; set; }
    }
}
