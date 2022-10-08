using FluentValidation;
using PAY_UP.Application.Dtos.Email;

namespace PAY_UP.Application.Validators.Email{
    public class ScheduleEmailDtoValidator : AbstractValidator<ScheduleEmailDto>{
        public ScheduleEmailDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.DebtorId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Message).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.ReminderType).IsInEnum().NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}