using CuidandoPawsApi.Application.DTOs.ServiceCatalog;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.ServiceCatalog
{
    public class UpdateServiceCatalogValidation : AbstractValidator<UpdateServiceCatalogDTos>
    {
        public UpdateServiceCatalogValidation()
        {
            RuleFor(x => x.NameService).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.NameService).MaximumLength(50).WithMessage("{PropertyName} cannot be more than 50 characters");

            RuleFor(x => x.DescriptionService).NotEmpty().WithMessage("{PropertyName} not null");

            RuleFor(x => x.Duration).NotEmpty().WithMessage("{PropertyName} not null");

            RuleFor(x => x.Price).NotEmpty().WithMessage("{PropertyName} not null");

            RuleFor(x => x.Type).NotEmpty().WithMessage("{PropertyName} not null");
        }
    } 
}
