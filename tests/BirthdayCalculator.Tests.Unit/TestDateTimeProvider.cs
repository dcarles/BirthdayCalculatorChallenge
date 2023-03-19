
using BirthdayCalculator.Domain.Models;
using System;

namespace BirthdayCalculator.Tests.Unit;

internal class TestDateTimeProvider : IDateTimeProvider
{
    public TestDateTimeProvider(DateTime today)
    {
        Now = today;
    }

    public DateTime Now { get; }
}
