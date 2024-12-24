using CuidandoPawsApi.Application.DTOs.Species;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Species
{
    public class CreateUpdateSpecies : AbstractValidator<CreateUpdateSpecieDTos>
    {
        public CreateUpdateSpecies()
        {
            RuleFor(x => x.DescriptionOfSpecies).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.DescriptionOfSpecies).MaximumLength(150).WithMessage("{PropertyName}cannot be more than 150 characters ");
        }
    }
}
