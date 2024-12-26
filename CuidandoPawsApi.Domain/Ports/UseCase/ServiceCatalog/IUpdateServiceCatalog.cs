using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.ServiceCatalog
{
    public interface IUpdateServiceCatalog<TDto, TDtoStatus>
    {
        Task <ResultT<TDto>> UpdateAsync(int id,TDtoStatus dto, CancellationToken cancellationToken);
    }
}
