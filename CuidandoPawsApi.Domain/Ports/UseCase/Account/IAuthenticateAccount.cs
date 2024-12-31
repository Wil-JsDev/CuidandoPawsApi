using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IAuthenticateAccount<TResponse, TRequest>
        where TResponse : class
        where TRequest : class
    {
        Task<TResponse> AuthenticateAsync(TRequest request);
    }
}
