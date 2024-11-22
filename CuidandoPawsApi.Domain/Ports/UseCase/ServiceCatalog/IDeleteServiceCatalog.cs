using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog
{
    public interface IDeleteServiceCatalog<TDto>
    {
        Task<TDto> DeleteAsync(int id);
    }
}
