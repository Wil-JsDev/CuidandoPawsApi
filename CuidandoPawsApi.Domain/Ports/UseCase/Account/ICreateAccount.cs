using CuidandoPawsApi.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface ICreateAccount <TResponse,TRequest>
        where TResponse : class 
        where TRequest : class
    {
        Task<TResponse> RegisterAccountAsync(TRequest resquest, string origin, Roles roles);
        
        Task<TResponse> RegisterAdminAsync(TRequest request, string origin);
    }
}
