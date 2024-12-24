using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord
{
    public interface IUpdateMedicalRecord<TDtoStatus,TDTo>
    {
        Task <ResultT<TDTo>> UpdateAsync(int id,TDtoStatus dtoStatus, CancellationToken cancellationToken);
    }
}
