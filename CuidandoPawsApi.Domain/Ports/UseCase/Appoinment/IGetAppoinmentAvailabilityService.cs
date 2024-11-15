using CuidandoPawsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IGetAppoinmentAvailabilityService<TDto>
    {
        Task<IEnumerable<TDto>> GetAvailabilityServiceAsync(int serviceCatalog, bool isActive, CancellationToken cancellationToken);
    }
}
