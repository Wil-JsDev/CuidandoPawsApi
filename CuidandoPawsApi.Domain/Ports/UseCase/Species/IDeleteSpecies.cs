using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Species
{
    public interface IDeleteSpecies<TDto>
    {
        Task<TDto> DeleteSpeciesAsync(int id, CancellationToken cancellationToken);
    }
}
