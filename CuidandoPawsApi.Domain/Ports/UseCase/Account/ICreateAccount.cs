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
        Task<TResponse> RegisterPetOwnerAsync(TRequest request, string origin);

        Task<TResponse> RegisterAdminAsync(TRequest request, string origin);

        Task<TResponse> RegisterCaregiverAsync(TRequest resquest, string origin);
    }
}
