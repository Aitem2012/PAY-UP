using FluentValidation;
using PAY_UP.Application.Dtos.Authentication;

namespace PAY_UP.Application.Validators.Authentication
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.NewPassword).NotEqual(x => x.OldPassword)
                .WithMessage("Old password and new password cannot be the same")
                .NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword)
                .WithMessage("Password does not match")
                .NotEmpty().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
