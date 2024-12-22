using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Account.JWT
{
    public class JwtResponse
    {
        public string? Error { get; set; }

        public bool? HasError { get; set; }
    }
}
