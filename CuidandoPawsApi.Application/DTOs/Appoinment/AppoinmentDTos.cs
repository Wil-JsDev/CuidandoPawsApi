using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Appoinment
{
    public class AppoinmentDTos
    {
        public int AppoinmentId { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public int ServiceCatalogId { get; set; }
    }
}
