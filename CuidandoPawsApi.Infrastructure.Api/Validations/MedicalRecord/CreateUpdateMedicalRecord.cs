using CuidandoPawsApi.Application.DTOs.MedicalRecord;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.MedicalRecord
{
    public class CreateUpdateMedicalRecord : AbstractValidator<CreateUpdateMedicalRecordDTos>
    {
        public CreateUpdateMedicalRecord()
        {
            RuleFor(x => x.Treatment).MaximumLength(100).WithMessage("{PrpertyName} cannot be more than 100 characters");
            RuleFor(x => x.Recommendations).MaximumLength(150).WithMessage("{PropertyName} cannot be more than 150 characters");
            RuleFor(x => x.IdPet).NotEmpty().WithMessage("{PropertyName} not null");
        }
    }
}
