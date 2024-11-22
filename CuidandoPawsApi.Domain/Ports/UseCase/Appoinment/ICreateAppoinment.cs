using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface ICreateAppoinment<TDtoStatus,TDto>
    {
        Task<TDto> AddAsync(TDtoStatus dtoStatus,CancellationToken cancellationToken);
    }
}
