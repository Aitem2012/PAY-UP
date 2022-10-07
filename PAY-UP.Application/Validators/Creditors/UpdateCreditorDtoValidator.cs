using FluentValidation;
using PAY_UP.Application.Dtos.Creditors;

namespace PAY_UP.Application.Validators.Creditors{
    public class UpdateCreditorDtoValidator : AbstractValidator<UpdateCreditorDto>{
        public UpdateCreditorDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{propertyName} is required");
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