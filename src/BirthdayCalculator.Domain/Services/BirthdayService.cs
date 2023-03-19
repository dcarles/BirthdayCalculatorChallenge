using BirthdayCalculator.Domain.Models;

namespace BirthdayCalculator.Domain.Services;

public class BirthdayService : IBirthdayService
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public BirthdayService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public bool IsBirthdayToday(DateTime birthdate)
    {
        //defensive check to avoid dates in future, although this should have been validated previously.
        //This could also throw an exception instead to be more explicit, but for simplicity we are returning false
        if (birthdate.Date > _dateTimeProvider.Now.Date)
        {
            return false;
        }

        var birthdateDay = birthdate.Day;

        //adjust birthdate for leap year so if a person was born on Feb 29th, then their birthday during a non-leap year should be considered to be Feb 28th
        if (birthdate.Month == 2 && birthdate.Day == 29 && !DateTime.IsLeapYear(_dateTimeProvider.Now.Year))
        {
            birthdateDay = 28;
        }

        return birthdate.Month == _dateTimeProvider.Now.Month && birthdateDay == _dateTimeProvider.Now.Day;
    }
}
