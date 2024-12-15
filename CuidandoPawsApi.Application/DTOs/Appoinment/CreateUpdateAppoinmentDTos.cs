using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Appoinment
{
    public class CreateUpdateAppoinmentDTos
    {
        public string? Notes { get; set; }

        public int IdServiceCatalog { get; set; }
    }
}
