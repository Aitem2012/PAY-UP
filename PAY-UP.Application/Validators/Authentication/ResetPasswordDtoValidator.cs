using FluentValidation;
using PAY_UP.Application.Dtos.Authentication;

namespace PAY_UP.Application.Validators.Authentication
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Token).NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password does not match")
                .NotEmpty().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
