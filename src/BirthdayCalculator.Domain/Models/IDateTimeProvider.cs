namespace BirthdayCalculator.Domain.Models;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
