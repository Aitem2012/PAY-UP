using FluentValidation;
using PAY_UP.Application.Dtos;

namespace PAY_UP.Application.Validators.Creditors{
    public class CreateCreditorDtoValidator : AbstractValidator<CreateCreditorDto>{
        public CreateCreditorDtoValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Lastname).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.DateCreditWasCollected).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.DateForRepayment).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.ReminderType).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}