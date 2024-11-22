using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.ServiceCatalog
{
    public class ServiceCatalogDTos
    {
        public int ServiceCatalogId { get; set; }

        public string? NameService { get; set; }

        public string? DescriptionService { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Type { get; set; }

        public bool IsAvaible { get; set; }

        public int Duration { get; set; }
    }
}
