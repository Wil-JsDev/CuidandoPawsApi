using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Species
{
    public interface IGetSpeciesLastAdded<TDto>
    {
        Task<TDto> GetLastAddedAsync(CancellationToken cancellationToken);
    }
}
