using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Pets
{
    public class CreatePetsDTos
    {
        public string? NamePaws { get; set; }

        public string? Bred {  get; set; }

        public int Age { get; set; }

        public string? Color { get; set; }

        public string? NotesPets { get; set; }

        public string? Gender { get; set; }

        public int SpeciesId { get; set; }
    }
}
