using CuidandoPawsApi.Application.DTOs.Pets;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Pets
{
    public class CreatePets : AbstractValidator<CreatePetsDTos>
    {
        public CreatePets()
        {
            RuleFor(x => x.NamePaws).NotEmpty().WithMessage("{ProperyName} not null");
            RuleFor(x => x.NamePaws).MaximumLength(50).WithMessage("{PropertyName} cannot be more than 50 characters");

            RuleFor(x => x.Bred).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Bred).MaximumLength(75).WithMessage("{PropertyName} cannot be more than 75 characters");

            RuleFor(x => x.NotesPets).NotEmpty().WithMessage("{ProperyName} not null");
            RuleFor(x => x.NotesPets).MaximumLength(75).WithMessage("{PropertyName} cannot be more than 75 characters");

            RuleFor(x => x.Color).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Color).MaximumLength(20).WithMessage("{PropertyName} cannot be more than 20 characters");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Gender).MaximumLength(25).WithMessage("{PropertyName} cannot be more than 25 characters");

            RuleFor(x => x.Age).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.SpeciesId).NotEmpty().WithMessage("{PropertyName} not null");
        }
    }
}
