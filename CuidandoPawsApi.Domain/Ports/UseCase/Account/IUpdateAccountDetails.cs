using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IUpdateAccountDetails<TDto, TStatusDto> 
        where TStatusDto : class
        where TDto : class
    {
        Task<TDto> UpdateAccountDetailsAsync(TStatusDto status, string id);
    }
}
