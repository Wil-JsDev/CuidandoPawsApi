using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Persistence.IOC
{
    public static class IOCPersistence
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<CuidandoPawsContext>(p =>
            {
                p.UseNpgsql(configuration.GetConnectionString("CuidadoPawsDb"));
            });
        }
    }
}
