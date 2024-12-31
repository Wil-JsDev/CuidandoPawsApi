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

        public async Task<ApiResponse<RegisterResponse>> RegisterAdminAsync(RegisterRequest request, string origin)
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
                var verification = await SendVerificationEmilUrlAsync(admin,origin);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = request.Email,
                    Body = $"<p>Welcome to the Admin Portal!</p>" +
                    $"<p>Please confirm your account by clicking the link below:</p>" +
                    $"<p><a href=\"{verification}\">{verification} Confirm Your Account</a></p>" +
                    $"<p>If you did not request this registration, please ignore this email.</p>",
                    Subject = "Confirm registration for admin"
                });
            }
            else
            {
                return ApiResponse<RegisterResponse>.ErrorResponse("An error ocurred trying to registed the user");
            }

            return ApiResponse<RegisterResponse>.SuccessResponse(response);
        }

        public async Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request, string origin, Roles roles)
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
                var verification = await SendVerificationEmilUrlAsync(user,origin);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = request.Email,
                    Body = $"<p>Please confirm your account by visiting this URL:</p><p><a href=\"{verification}\">{verification}</a></p>",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                return ApiResponse<RegisterResponse>.ErrorResponse("An error ocurred trying to registed the user");
            }

            return ApiResponse<RegisterResponse>.SuccessResponse(response);
        }

        private async Task<string> SendVerificationEmilUrlAsync(User user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string route = "User/ConfirmEmail";

            var uri = new Uri(string.Concat($"{origin}/", route));

            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }
    }
}
