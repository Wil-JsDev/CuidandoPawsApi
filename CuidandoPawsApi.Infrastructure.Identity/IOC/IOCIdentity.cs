using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.JWT;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Settings;
using CuidandoPawsApi.Infrastructure.Identity.Adapters;
using CuidandoPawsApi.Infrastructure.Identity.Context;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

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

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        //When the code explodes, because something was not handled
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        //You are not authorized or it was an invalid token
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized"});
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        //It is for when the user does not have permission to enter
                        c.Response.StatusCode = 403;
                        c.Response.ContentType  = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not authorized to access this resource"});
                        return c.Response.WriteAsync(result);
                    }


                };
            });


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
