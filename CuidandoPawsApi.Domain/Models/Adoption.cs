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

        public DateTime AdoptionDate { get; set; }

        public bool status { get; set; }

        public string? Notes { get; set; }
    }
}
