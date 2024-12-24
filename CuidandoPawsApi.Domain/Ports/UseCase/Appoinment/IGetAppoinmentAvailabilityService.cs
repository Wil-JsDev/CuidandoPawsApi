using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IGetAppoinmentAvailabilityService<TDto>
    {
        Task <ResultT<IEnumerable<TDto>>> GetAvailabilityServiceAsync(int serviceCatalog, CancellationToken cancellationToken);
    }
}
