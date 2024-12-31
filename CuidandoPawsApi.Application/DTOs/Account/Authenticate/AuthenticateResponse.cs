using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.DTOs.Account.Authenticate
{
    public class AuthenticateResponse
    {
       public string? Id { get; set; }

       public string? FirstName { get; set; }

       public string? LastName { get; set; }

       public string? Email { get; set; }

       public string? Username { get; set; }

       public List<string>? Roles { get; set; }

       public bool? IsVerified { get; set; }
         
       public int StatusCode {  get; set; } 

       public string? JWTToken { get; set; }

       public string? PhoneNumber { get; set; }

       [JsonIgnore] 
       public string? RefreshToken { get; set; }
    }
}
