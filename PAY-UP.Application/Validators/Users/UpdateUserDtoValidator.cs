using FluentValidation;
using PAY_UP.Application.Dtos.Users;

namespace PAY_UP.Application.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}
