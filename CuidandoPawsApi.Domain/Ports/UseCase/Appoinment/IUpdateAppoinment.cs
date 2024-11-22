using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IUpdateAppoinment<TDtoStatus,TDto>
    {
        Task<TDto> UpdateAsync(int id,TDtoStatus dtoStatus, CancellationToken cancellationToken);
    }
}
