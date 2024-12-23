using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Repository
{
    public interface ISpeciesRepository : IGenericRepository<Species>
    {
        Task<Species> GetLastAddedSpeciesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Species>> GetOrdereByIdAscSpeciesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Species>> GetOrdereByIdDescSpeciesAsync(CancellationToken cancellationToken);
    }
}
