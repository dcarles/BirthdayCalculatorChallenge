using BirthdayCalculator.Domain.Services;
using FluentAssertions;
using System;
using System.Globalization;
using Xunit;

namespace BirthdayCalculator.Tests.Unit;

public class BirthdayServiceTests
{
    [Theory, MemberData(nameof(BirthdateData))]
    public void IsBirthdayToday_PersonBornToday_ReturnsTrue(DateTime birthdate, DateTime today, bool expectedResult)
    {
        //arrange
        var birthdayService = new BirthdayService(new TestDateTimeProvider(today));

        //act
        var result = birthdayService.IsBirthdayToday(birthdate);

        //assert
        result.Should().Be(expectedResult);
    }

    public static readonly object[][] BirthdateData =
                    {
                                    //birthdate, today's date, expected result
                      new object[] { new DateTime(2024, 3, 19), new DateTime(2023, 3, 19), false}, //Birthdate is in the future so should not count
                      new object[] { new DateTime(2000, 3, 19), new DateTime(2023, 3, 19), true}, //Is birthday today
                      new object[] { new DateTime(2000, 3, 19), new DateTime(2023, 3, 21), false}, //Is not birthday today
                      new object[] { new DateTime(2020, 2, 29), new DateTime(2023, 2, 28), true}, // leap year adjustment
                      new object[] { new DateTime(2020, 2, 29), new DateTime(2024, 2, 29), true}, // Last day of feb, both leap years
                      new object[] { new DateTime(1983, 2, 28), new DateTime(2020, 2, 29), false}, //Last day of feb non leap year compared with a last day feb leap year
                      new object[] { new DateTime(1983, 2, 28), new DateTime(2023, 2, 28), true}, // Last day of feb, both non leap years
                    };
}
