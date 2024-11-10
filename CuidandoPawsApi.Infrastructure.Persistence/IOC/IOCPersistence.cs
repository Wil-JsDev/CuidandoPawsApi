using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository;

namespace CuidandoPawsApi.Infrastructure.Persistence.IOC
{
    public static class IOCPersistence
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddDbContext<CuidandoPawsContext>(p =>
            {
                p.UseNpgsql(configuration.GetConnectionString("CuidadoPawsDb"), b =>
                {
                    b.MigrationsAssembly("CuidandoPawsApi.Infrastructure.Persistence");
                });
            });
            #endregion
            
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAppoinmentRepository, AppoinmentRepository>();
            services.AddTransient<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddTransient<IPetsRepository, PetsRepository>();
            services.AddTransient<IServiceCatalogRepository, ServiceCatalogRepository>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            #endregion
        }
    }
}
