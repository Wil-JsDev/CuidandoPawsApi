using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Adapters
{
    public class UpdateAccountDetails : IUpdateAccountDetails<AccountDto, UpdateAccountDTo>
    {

        private readonly UserManager<User> _userManager;

        public UpdateAccountDetails(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountDto> UpdateAccountDetailsAsync(UpdateAccountDTo status, string id)
        {
            var userId = await _userManager.FindByIdAsync(id);

            if (userId != null)
            {
                User user = new()
                {
                    FirstName = status.FirstName,
                    LastName = status.LastName,
                    PhoneNumber = status.PhoneNumber
                };

                var updateUser = await _userManager.UpdateAsync(user);

                AccountDto accountDto = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };

                return accountDto;
            }

            return null;
        }
    }
}
