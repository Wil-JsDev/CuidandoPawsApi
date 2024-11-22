using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository
{
    public class ServiceCatalogRepository : GenericRepository<ServiceCatalog>, IServiceCatalogRepository
    {
        public ServiceCatalogRepository(CuidandoPawsContext context) : base(context) { }   
    }
}
