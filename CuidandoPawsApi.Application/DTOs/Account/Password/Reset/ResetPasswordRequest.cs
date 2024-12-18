using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Account.Password.Reset
{
    public class ResetPasswordRequest
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? Password { get; set; }
    }
}
