using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Account.Authenticate
{
    public class AuthenticateRequest
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
