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
    public class UpdateAccountDetails : IUpdateAccountDetails<AccountDto,UpdateAccountDTo>
    {

        private readonly UserManager<User> _userManager;

        public UpdateAccountDetails(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<AccountDto>> UpdateAccountDetailsAsync(UpdateAccountDTo status, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.FirstName = status.FirstName;
                user.LastName = status.LastName;
                user.PhoneNumber = status.PhoneNumber;
                user.UserName = status.Username;
                
                var updateUser = await _userManager.UpdateAsync(user);

                AccountDto accountDto = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName, 
                    PhoneNumber = user.PhoneNumber,
                    Username = user.UserName,
                    Email = user.Email,
                    CreateAt = user.CreateAt
                };

                return ApiResponse<AccountDto>.SuccessResponse(accountDto);
            }

            return ApiResponse<AccountDto>.ErrorResponse($"this {id} account not found");
        }
    }
}
