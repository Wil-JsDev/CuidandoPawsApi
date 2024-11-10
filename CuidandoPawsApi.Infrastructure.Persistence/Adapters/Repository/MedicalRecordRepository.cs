using CuidandoPawsApi.Domain.Models;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Infrastructure.Persistence.Context;

namespace CuidandoPawsApi.Infrastructure.Persistence.Adapters.Repository;

public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(CuidandoPawsContext context) : base(context)
    {
        
    }
}