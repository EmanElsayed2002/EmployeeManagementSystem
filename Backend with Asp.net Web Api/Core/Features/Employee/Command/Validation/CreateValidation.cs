using Core.Features.Employee.Command.Models;
using FluentValidation;

namespace Core.Features.Employee.Command.Validation
{
    public class CreateValidation : AbstractValidator<CreateEmployee>
    {
        public CreateValidation()
        {
            RuleFor(x => x.Email)
    .NotEmpty().WithMessage("Email is required.")
    .EmailAddress().WithMessage("Invalid email format.");


            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Your First Name is Required");
            RuleFor(x => x.Position).NotEmpty().WithMessage("Your Position Must be Not Empty");
        }
    }
}
