using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Account.Register
{
    public class RegisterResponse
    {
        public int StatusCode { get; set; }

        public string? Error { get; set; }

        public bool? HasError { get; set; }
    }
}
