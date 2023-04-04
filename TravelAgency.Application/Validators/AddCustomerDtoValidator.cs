using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Application.Models.Customer;

namespace TravelAgency.Application.Validators
{
    public class AddCustomerDtoValidator : AbstractValidator<AddCustomerDto>
    {
        public AddCustomerDtoValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.")
                .Matches(@"^[\p{L}]+$").WithMessage("First name can only contain letters.");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.")
                .Matches(@"^[\p{L}]+$").WithMessage("Last name can only contain letters."); ;

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{9,20}$").WithMessage("Invalid phone number format.");

            RuleFor(dto => dto.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(10, 255).WithMessage("Address must be between 10 and 200 characters.");
        }
    }
}
