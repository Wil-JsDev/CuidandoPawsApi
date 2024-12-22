using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IGetByIdAppoinment<TDto>
    {
        Task <ResultT<TDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
