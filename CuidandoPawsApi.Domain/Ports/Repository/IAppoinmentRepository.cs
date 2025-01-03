﻿using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Repository
{
    public interface IAppoinmentRepository : IGenericRepository<Appoinment>
    {
        Task<IEnumerable<ServiceCatalog>> CheckAvailabilityAsync(int serviceId, CancellationToken cancellationToken);

        Task<IEnumerable<ServiceCatalog>> GetAvailabilityServiceAsync (ServiceCatalog serviceCatalog, bool isActive, CancellationToken cancellationToken);
        
        Task<Appoinment> GetLastAppoinmentAddedOnDateAsync(DateTime dateTime, CancellationToken cancellationToken);
    }
}
