using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Account
{
    public interface IConfirmAccount
    {
        Task<ApiResponse<string>> ConfirmAccountAsync(string userId, string token);
    }
}
