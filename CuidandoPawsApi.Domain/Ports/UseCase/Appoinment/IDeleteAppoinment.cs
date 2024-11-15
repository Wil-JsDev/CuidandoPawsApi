using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IDeleteAppoinment<TDto>
    {
        Task<TDto> DeleteAppoinmentAsync(int id, CancellationToken cancellationToken);
    }
}
