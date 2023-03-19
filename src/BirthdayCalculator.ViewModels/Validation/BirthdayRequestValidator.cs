using FluentValidation;

namespace BirthdayCalculator.ViewModels.Validation;

public class BirthdayRequestValidator : AbstractValidator<BirthdayRequest>
{
    public BirthdayRequestValidator()
    {
        RuleSet("Default, Post", () =>
        {
            RuleFor(request => request.People).NotNull();
            RuleForEach(p => p.People).Must(a => true).SetValidator(new PersonValidator());
        });
    }
}