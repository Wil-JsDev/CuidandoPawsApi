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
    public class Logout : ILogout
    {
        
        private readonly SignInManager<User> _signInManager;

        public Logout(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task LogOutAsync()
        {
          await _signInManager.SignOutAsync();
        }
    }
}
