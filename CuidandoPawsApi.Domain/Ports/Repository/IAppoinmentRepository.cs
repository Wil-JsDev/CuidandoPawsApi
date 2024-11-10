using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Repository
{
    public interface IAppoinmentRepository : IGenericRepository<Appoinment>
    {
        Task<IEnumerable<ServiceCatalog>> CheckAvailabilityAsync(int serviceId, DateTime date);

        Task<IEnumerable<ServiceCatalog>> GetAvailabilityServiceAsync (int serviceCatalog, bool isActive);
        
        Task<Appoinment> GetLastAddedAppoinmentAsync (int appoinmentId);
    }
}
