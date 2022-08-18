using FluentValidation;
using PAY_UP.Application.Dtos.Email;

namespace PAY_UP.Application.Validators.Email
{
    public class EmailRequestDtoValidator : AbstractValidator<EmailRequestDto>
    {
        public EmailRequestDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.RecipientEmail).EmailAddress().NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.ScheduleType).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Message).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Subject).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}
