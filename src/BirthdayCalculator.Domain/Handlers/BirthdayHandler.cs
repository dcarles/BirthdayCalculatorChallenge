using BirthdayCalculator.Domain.Commands;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.Domain.Services;
using MediatR;

namespace BirthdayCalculator.Domain;

public class BirthdayHandler : IRequestHandler<CalculateBirthdaysCommand, IEnumerable<Person>>
{
    private readonly IBirthdayService _birthdayService;

    public BirthdayHandler(IBirthdayService birthdayService)
    {
        _birthdayService = birthdayService;
    }

    public Task<IEnumerable<Person>> Handle(CalculateBirthdaysCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.People == null)
            return Task.FromResult(Enumerable.Empty<Person>());

        return Task.Run(() => GetBirthdayCelebrants(request.People));
    }

    private IEnumerable<Person> GetBirthdayCelebrants(IEnumerable<Person> people)
    {
        foreach (var person in people)
        {
            if (_birthdayService.IsBirthdayToday(person.BirthDate))
                yield return person;
        }
    }
}
