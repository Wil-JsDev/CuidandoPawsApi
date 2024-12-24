using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord
{
    public interface IDeleteMedicalRecord<TDto>
    {
        Task <ResultT<TDto>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
