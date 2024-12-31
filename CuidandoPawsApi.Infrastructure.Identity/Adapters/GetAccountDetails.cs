using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Utils;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Adapters
{
    public class GetAccountDetails : IGetAccountDetails<AccountDto>
    {
        private readonly UserManager<User> _userManager;

        public GetAccountDetails(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<AccountDto>> GetAccountDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                AccountDto accountDto = new()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    CreateAt = user.CreateAt
                };

                return ApiResponse<AccountDto>.SuccessResponse(accountDto);

            }

            return ApiResponse<AccountDto>.ErrorResponse($"this {userId} account not found");
        }
    }
}
