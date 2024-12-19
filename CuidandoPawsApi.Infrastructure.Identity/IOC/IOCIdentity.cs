using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Infrastructure.Identity.Adapters;
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

            #region AccountUseCase
            services.AddScoped<ICreateAccount<RegisterResponse, RegisterRequest>, CreateAccount>();
            services.AddScoped<IConfirmAccount, ConfirmAccount>();
            services.AddScoped<IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest>, AuthenticateAccount>();
            services.AddScoped<ILogout,Logout>();
            services.AddScoped<IForgotPassword<ForgotResponse, ForgotRequest>,ForgotPassword>();
            services.AddScoped<IResetPassword<ResetPasswordResponse, ResetPasswordRequest>, ResetPassword>();
            services.AddScoped<IGetAccountDetails<AccountDto>, GetAccountDetails>();
            services.AddScoped<IUpdateAccountDetails<AccountDto,UpdateAccountDTo>, UpdateAccountDetails>();
            services.AddScoped<IDeleteAccount, DeleteAccount>();
            #endregion
        }
    }
}
