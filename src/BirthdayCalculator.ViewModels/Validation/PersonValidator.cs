using FluentValidation;

namespace BirthdayCalculator.ViewModels.Validation;

public class PersonValidator : AbstractValidator<PersonDTO>
{
    public PersonValidator()
    {
        RuleSet("Default, Post", () =>
        {
            RuleFor(request => request.FirstName).NotEmpty();
            RuleFor(request => request.LastName).NotEmpty();
            RuleFor(request => request.BirthDate).NotNull().WithMessage("Birthdate is empty or has an invalid format").LessThan(DateTime.Today);
        });
    }
}
