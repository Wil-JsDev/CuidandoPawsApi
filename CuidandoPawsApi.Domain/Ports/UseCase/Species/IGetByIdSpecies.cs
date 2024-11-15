using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Species
{
    public interface IGetByIdSpecies <TDto>
    {
        Task<TDto> GetById (int id, CancellationToken cancellationToken);
    }
}
