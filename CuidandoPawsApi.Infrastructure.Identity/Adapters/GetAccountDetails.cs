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
    public class GetAccountDetails : IGetAccountDetails<AccountDto>
    {
        private readonly UserManager<User> _userManager;

        public GetAccountDetails(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountDto> GetAccountDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

               return new ()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
                
            }

            return null;
        }
    }
}
