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
    public class ConfirmAccount : IConfirmAccount
    {
        private readonly UserManager<User> _userManager;

        public ConfirmAccount(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse($"No account registered with this {userId} user id");
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded)
            {
                return ApiResponse<string>.SuccessResponse($"Account confirm for {user.Email}. You can now use the app");
            }
            else
            {
                return ApiResponse<string>.ErrorResponse($"An error occurred while confirming {user.Email}");
            }
        }
    }
}
