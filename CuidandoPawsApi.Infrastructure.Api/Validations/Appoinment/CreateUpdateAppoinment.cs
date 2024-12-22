using CuidandoPawsApi.Application.DTOs.Appoinment;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Appoinment
{
    public class CreateUpdateAppoinment : AbstractValidator<CreateUpdateAppoinmentDTos>
    {
        public CreateUpdateAppoinment()
        {
            RuleFor(x => x.Notes).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Notes).MaximumLength(75).WithMessage("{PropertyName} cannot be more than 75 characters");
            RuleFor(x => x.IdServiceCatalog).NotEmpty().WithMessage("{PropertyName} not null");
        }
    }
}
