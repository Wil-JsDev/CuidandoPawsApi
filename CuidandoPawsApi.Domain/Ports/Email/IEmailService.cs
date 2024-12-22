using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Ports.Email
{
    public interface IEmailService<TStatusDTo>
    {
        Task Execute(TStatusDTo dto);
    }
}
