﻿using CuidandoPawsApi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.UseCase.Species
{
    public interface IGetSpeciesOrderById<TDTos>
    {
        Task <ResultT<IEnumerable<TDTos>>> GetOrderedByIdAsync(string sort,string direction,CancellationToken cancellationToken);
    }
}
