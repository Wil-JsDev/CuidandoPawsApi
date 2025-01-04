using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Account
{
    public class ResetPasswordRequestValidation : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} it is not an email");

            RuleFor(x => x.Token).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
