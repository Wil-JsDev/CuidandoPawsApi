using CuidandoPawsApi.Infrastructure.Identity.Context;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CuidandoPawsApi.Infrastructure.Identity.IOC
{
    public static class IOCIdentity
    {

        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            #region IdentityContext
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("CuidandoPaswsDbIndetity"), 
                    b => b.MigrationsAssembly("CuidandoPawsApi.Infrastructure.Identity"));
            });

            #endregion

            #region Identity
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
                                                       .AddDefaultTokenProviders();

            #endregion
        }
    }
}
