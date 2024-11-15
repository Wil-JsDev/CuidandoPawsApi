using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Pets
{
    public interface IGetPetsLastAddedOfDay<TDto>
    {
        Task<TDto> GetLastAddedPetsOfDayAsync(CancellationToken cancellationToken);
    }
}
