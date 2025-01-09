using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Utils;
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

        public async Task<ApiResponse<ForgotResponse>> GetForgotPasswordAsync(ForgotRequest request)
        {
           
            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                return ApiResponse<ForgotResponse>.ErrorResponse($"Account no registered with {request.Email}");
            }

            var verification = await SendForgotPasswordAsync(account);
            await _emailSender.Execute(new EmailRequestDTos
            {
                To = account.Email,
                Body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333; line-height: 1.8; max-width: 600px; margin: 0 auto; border: 1px solid #e6e6e6; border-radius: 10px; padding: 25px; background-color: #ffffff;'>
                    <h1 style='color: #007BFF; font-size: 26px; margin-bottom: 20px; text-align: center;'Forget your password</h1>
                    <p style='font-size: 16px; margin-bottom: 20px; text-align: center;'>
                        Hello <strong>{request.Email}</strong>, <br>
                        We know it's easy to forget passwords sometimes. Don't worry, we're here to help.
                    </p>
                    <div style='font-size: 16px; background-color: #f9f9f9; padding: 20px; border: 1px dashed #ccc; border-radius: 8px; margin-bottom: 20px; text-align: center;'>
                    <strong style='color: #333;'>Your Verification Token:</strong>
                    <p style='font-size: 18px; color: #007BFF; font-weight: bold;'>{verification}</p>
                    </div>
                    <p style='font-size: 14px; margin-bottom: 20px; text-align: center;'>
                    If you didn’t request this email, no further action is required. Please feel free to contact us if you have any concerns.
                    </p>
                    <hr style='border: none; border-top: 1px solid #e6e6e6; margin: 30px 0;'>
                    <p style='font-size: 12px; color: #888; text-align: center;'>
                    This email is brought to you by <strong>Cuidando Paws</strong>. <br>
                    Please don't worry about your password.
                    </p>
                            </div>",
                Subject = "Forgot Password"
            });


            ForgotResponse response = new();
            response.Message = "Email sent. Check your inbox.";
            return ApiResponse<ForgotResponse>.SuccessResponse(response);
        }

        private async Task<string> SendForgotPasswordAsync(User user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            return code;
        }

    }
}
