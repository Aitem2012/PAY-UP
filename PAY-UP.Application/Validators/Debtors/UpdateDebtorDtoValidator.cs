using FluentValidation;
using PAY_UP.Application.Dtos.Debtors;

namespace PAY_UP.Application.Validators{
    public class UpdateDebtorDtoValidator : AbstractValidator<UpdateDebtorDto>{
        public UpdateDebtorDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Firstname).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Lastname).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.DateCreditWasCollected).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.DateForRepayment).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.ReminderType).IsInEnum().NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.AmountOwed).GreaterThan(0).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.AppUserId).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.AmountPaid).GreaterThan(0).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}