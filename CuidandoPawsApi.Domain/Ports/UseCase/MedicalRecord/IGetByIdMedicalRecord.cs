using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord
{
    public interface IGetByIdMedicalRecord<TDto>
    {
        Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
