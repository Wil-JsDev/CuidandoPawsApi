using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Utils;
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
        Task<ApiResponse<TResponse>> RegisterAccountAsync(TRequest resquest, Roles roles);
        
        Task<ApiResponse<TResponse>> RegisterAdminAsync(TRequest request);
    }
}
