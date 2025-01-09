using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Utils;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Adapters
{
    public class CreateAccount : ICreateAccount<RegisterResponse, RegisterRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService<EmailRequestDTos> _emailSender;

        public CreateAccount(UserManager<User> userManager, SignInManager<User> signInManager, 
            IEmailService<EmailRequestDTos> emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<ApiResponse<RegisterResponse>> RegisterAdminAsync(RegisterRequest request)
        {
            RegisterResponse response = new();

            var userWithSameUsername = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                return ApiResponse<RegisterResponse>.ErrorResponse($"this user {userWithSameUsername} is already taken");
            }

            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                return ApiResponse<RegisterResponse>.ErrorResponse($"this email {userWithEmail} is already taken"); ;
            }

            User admin = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                CreateAt = DateTime.UtcNow
            };
          
            var result = await _userManager.CreateAsync(admin,request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin,Roles.Admin.ToString());
                response.Email = request.Email;
                response.Username = request.Username;
                response.UserId = admin.Id;
                var verification = await SendVerificationEmilUrlAsync(admin);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = request.Email,
                    Body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333; line-height: 1.8; max-width: 600px; margin: 0 auto; border: 1px solid #e6e6e6; border-radius: 10px; padding: 25px; background-color: #ffffff;'>
                    <h1 style='color: #007BFF; font-size: 26px; margin-bottom: 20px; text-align: center;'>Confirm Your Account Registration</h1>
                    <p style='font-size: 16px; margin-bottom: 20px; text-align: center;'>
                        Hello <strong>{request.Email}</strong>, <br>
                        Thank you very much for registering as admin, taking care of paws is a pleasure. To complete your account setup, please use the verification token provided below.
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
                    This email is brought to you by <strong>Caring for Paws</strong>. <br>
                    Please do not reply directly to this email as it is not monitored.
                    </p>
                            </div>",
                    Subject = "Confirm registration for admin"
                });
            }
            else
            {
                return ApiResponse<RegisterResponse>.ErrorResponse("An error ocurred trying to registed the user");
            }

            return ApiResponse<RegisterResponse>.SuccessResponse(response);
        }

        public async Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request, Roles roles)
        {
            RegisterResponse response = new();

            var username = await _userManager.FindByNameAsync(request.Username);
            if (username != null)
            {
                return ApiResponse<RegisterResponse>.ErrorResponse($"this user {request.Username} is already taken");
            }

            var userWithEmial = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmial != null)
            {
                return ApiResponse<RegisterResponse>.ErrorResponse($"this email {request.Email} is already taken");
            }

            User user = new ()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreateAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                response.Email = request.Email;
                response.Username = request.Username;
                response.UserId = user.Id;

                await _userManager.AddToRoleAsync(user, roles.ToString());
                var verification = await SendVerificationEmilUrlAsync(user);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = request.Email,
                    Body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333; line-height: 1.8; max-width: 600px; margin: 0 auto; border: 1px solid #e6e6e6; border-radius: 10px; padding: 25px; background-color: #ffffff;'>
                    <h1 style='color: #007BFF; font-size: 26px; margin-bottom: 20px; text-align: center;'>Confirm Your Account Registration</h1>
                    <p style='font-size: 16px; margin-bottom: 20px; text-align: center;'>
                        Hello <strong>{request.Email}</strong>, <br>
                        Thank you for registering with us. To complete your account setup, please use the verification token provided below.
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
                    This email is brought to you by <strong>Caring for Paws</strong>. <br>
                    Please do not reply directly to this email as it is not monitored.
                    </p>
                            </div>",
                    Subject = "Confirm Your Account Registration"
                });
            }
            else
            {
                return ApiResponse<RegisterResponse>.ErrorResponse("An error ocurred trying to registed the user");
            }

            return ApiResponse<RegisterResponse>.SuccessResponse(response);
        }

        private async Task<string> SendVerificationEmilUrlAsync(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return code;
        }
    }
}
