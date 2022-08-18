using FluentValidation;
using PAY_UP.Application.Dtos;

namespace PAY_UP.Application.Validators.SmS
{
    public class SmSDtoValidator : AbstractValidator<SmSDto>
    {
        public SmSDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Message).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.PhoneNumber).Must(x => x.Length == 11).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.ScheduleType).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}
