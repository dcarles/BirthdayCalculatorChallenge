using BirthdayCalculator.Domain.Models;
using MediatR;

namespace BirthdayCalculator.Domain.Commands;

public class CalculateBirthdaysCommand : IRequest<IEnumerable<Person>>
{
    public IEnumerable<Person>? People { get; set; }
}
