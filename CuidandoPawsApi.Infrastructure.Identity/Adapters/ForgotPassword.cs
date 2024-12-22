using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Adapters
{
    public class ForgotPassword : IForgotPassword<ForgotResponse, ForgotRequest>
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService<EmailRequestDTos> _emailSender;

        public ForgotPassword(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService<EmailRequestDTos> emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<ForgotResponse> GetForgotPasswordAsync(ForgotRequest request, string origin)
        {
            ForgotResponse response = new()
            {
                HasError = false
            };
           
            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"Account no registered with {account.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordAsync(account,origin);
            await _emailSender.Execute(new EmailRequestDTos
            {
                To = account.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "Forgot Password"
            });
                
            return response;
        }

        private async Task<string> SendForgotPasswordAsync(User user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "user/ResetPassword";

            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "token", code);
            return verificationUri;
        }

    }
}
