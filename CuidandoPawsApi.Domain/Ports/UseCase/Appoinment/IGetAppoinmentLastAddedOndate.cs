using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Appoinment
{
    public interface IGetAppoinmentLastAddedOndate<TDto>
    {
        Task <ResultT<TDto>> GetLastAddedOnDateAsync(FilterDate filterDate,CancellationToken cancellationToken);
    }
}
