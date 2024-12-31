using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IUpdateAccountDetails<DTo,TStatusDto> 
        where TStatusDto : class
        where DTo : class
    {
        Task<ApiResponse<DTo>> UpdateAccountDetailsAsync(TStatusDto status, string id);
    }
}
