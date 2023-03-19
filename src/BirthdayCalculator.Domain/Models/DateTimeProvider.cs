namespace BirthdayCalculator.Domain.Models;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
