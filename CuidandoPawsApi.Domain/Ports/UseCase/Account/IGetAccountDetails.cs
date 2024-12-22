using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IGetAccountDetails<TDto> where TDto : class
    {
        Task<TDto> GetAccountDetailsAsync(string userId);
    }
}
