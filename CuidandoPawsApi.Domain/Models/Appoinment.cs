using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Models
{
    public class Appoinment
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }
    }
}
