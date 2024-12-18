using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IResetPassword<TResponse, TRequest>
    {
        Task<TResponse> ResetPasswordAsync(TRequest request);
    }
}
