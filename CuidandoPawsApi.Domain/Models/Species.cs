using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Models
{
    public class Species
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime? EntryOfSpecie { get; set; }

        public ICollection<Pets>? Pets { get; set; }
    }
}
