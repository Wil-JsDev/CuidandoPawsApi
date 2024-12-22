using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Enum;
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

        public async Task<RegisterResponse> RegisterAdminAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUsername = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                response.HasError = true;
                response.StatusCode = 400;
                response.Error = $"this user {userWithSameUsername} is already taken";
                return response;
            }

            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                response.StatusCode = 400;
                response.HasError = true;
                response.Error = $"this email {userWithEmail} is already taken";
                return response;
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
                response.StatusCode = 200;
                await _userManager.AddToRoleAsync(admin,Roles.Admin.ToString());
                var verification = await SendVerificationEmilUrlAsync(admin,origin);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = request.Email,
                    Body = $"<p>Welcome to the Admin Portal!</p>" +
                    $"<p>Please confirm your account by clicking the link below:</p>" +
                    $"<p><a href=\"{verification}\">Confirm Your Account</a></p>" +
                    $"<p>If you did not request this registration, please ignore this email.</p>",
                    Subject = "Confirm registration for admin"
                });
            }
            else
            {
                response.StatusCode = 500;
                response.HasError = true;
                response.Error = "An error ocurred trying to registed the user";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterAccountAsync(RegisterRequest resquest, string origin, Roles roles)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var username = await _userManager.FindByNameAsync(resquest.Username);
            if (username != null)
            {
                response.StatusCode = 400;
                response.HasError = true;
                response.Error = $"this user {username} is already taken";
                return response;
            }

            var userWithEmial = await _userManager.FindByEmailAsync(resquest.Email);
            if (userWithEmial != null)
            {
                response.StatusCode = 400;
                response.HasError = true;
                response.Error = $"this email {userWithEmial} is already taken";
                return response;
            }

            User caregiver = new ()
            {
                FirstName = resquest.FirstName,
                LastName = resquest.LastName,
                UserName = resquest.Username,
                Email = resquest.Email,
                CreateAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(caregiver, resquest.Password);
            if (result.Succeeded)
            {
                response.StatusCode = 200;
                await _userManager.AddToRoleAsync(caregiver, roles.ToString());
                var verification = await SendVerificationEmilUrlAsync(caregiver,origin);
                await _emailSender.Execute(new EmailRequestDTos
                {
                    To = resquest.Email,
                    Body = $"<p>Please confirm your account by visiting this URL:</p><p><a href=\"{verification}\">{verification}</a></p>",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.StatusCode = 500;
                response.HasError = true;
                response.Error = "An error ocurred trying to registed the user";
                return response;
            }

            return response;
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
