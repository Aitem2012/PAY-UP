using FluentValidation;
using PAY_UP.Application.Dtos.Users;

namespace PAY_UP.Application.Validators.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.PhoneNumber).Must(x => x.Length.Equals(11)).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}
