using CuidandoPawsApi.Application.DTOs.Account.Register;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Account
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} not null");

            RuleFor(x => x.Username).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} it is not an email");

            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
