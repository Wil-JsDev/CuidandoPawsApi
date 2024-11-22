﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.MedicalRecord
{
    public interface ICreateMedicalRecord<TDtoStatus, TDto>
    {
        Task<TDto> AddAsync(TDtoStatus dtoStatus, CancellationToken cancellationToken);
    }
}
