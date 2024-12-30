using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
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
    public class ResetPassword : IResetPassword<ResetPasswordResponse, ResetPasswordRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ResetPassword(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                return ApiResponse<ResetPasswordResponse>.ErrorResponse($"No account register with {request.Email}");
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);

            if (!result.Succeeded)
            {
                return ApiResponse<ResetPasswordResponse>.ErrorResponse("An error occurred while resseting");
            }

            ResetPasswordResponse response = new();
            response.Message = "Your password has been changed, you can use the app";
            return ApiResponse<ResetPasswordResponse>.SuccessResponse(response);
        }
    }
}
