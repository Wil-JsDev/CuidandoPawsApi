using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IForgotPassword<TResponse,TRequest> 
        where TResponse : class
        where TRequest : class
    {
        Task<ApiResponse<TResponse>> GetForgotPasswordAsync(TRequest request);
    }
}
