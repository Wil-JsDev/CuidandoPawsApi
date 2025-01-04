using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using FluentValidation;

namespace CuidandoPawsApi.Infrastructure.Api.Validations.Account
{
    public class ForgotRequestValidation : AbstractValidator<ForgotRequest>
    {
        public ForgotRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} not null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} it is not an email ");
        }
    }
}
