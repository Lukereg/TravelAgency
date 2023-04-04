using FluentValidation;
using TravelAgency.Application.Models.Order;

namespace TravelAgency.Application.Validators
{
    public class AddOrderDtoValidator : AbstractValidator<AddOrderDto>
    {
        public AddOrderDtoValidator()
        {
            RuleFor(x => x.TourName)
                .NotEmpty().WithMessage("Tour name is required.")
                .MaximumLength(70).WithMessage("Tour name cannot be longer than 70 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .Must(BeAValidDate).WithMessage("Start date must be a valid date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .Must(BeAValidDate).WithMessage("End date must be a valid date.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be greater than start date.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(x => x.CreatorId)
                .NotEmpty().WithMessage("Creator ID is required.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }

}
