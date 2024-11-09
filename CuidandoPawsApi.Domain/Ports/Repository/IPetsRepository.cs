using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Repository
{
    public interface IPetsRepository : IGenericRepository<Pets>
    {

        Task<IPagedResult<Pets>> GetPagedPetsAsync(int pageNumber, int pageSize);

        Task<Pets> GetLastAddedPetAsync(Species specie);
    }
}
