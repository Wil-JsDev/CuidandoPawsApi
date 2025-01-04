using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Account
{
    public class AuthenticateRequestValidation : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} it is not an email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} password is required");
        }
    }
}
