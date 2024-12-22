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
    public class DeleteAccount : IDeleteAccount
    {

        private readonly UserManager<User> _userManager;

        public DeleteAccount(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task RemoveAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
