using AutoFixture;
using BirthdayCalculator.Domain;
using BirthdayCalculator.Domain.Commands;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BirthdayCalculator.Tests.Unit;

public class BirthdayHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IBirthdayService> _serviceMock;

    public BirthdayHandlerTests()
    {
        _fixture = new Fixture();
        _serviceMock = new Mock<IBirthdayService>();
    }

    [Theory]
    [InlineData(true, 3)]
    [InlineData(false, 0)]
    public async Task HandleAsync_IsBirthdayTodayCalled_ReturnsBirthdayCelebrants(bool isBirthday, int expectedBirthdayCelebrants)
    {
        //arrange
        var people = _fixture.CreateMany<Person>(3);
        var command = new CalculateBirthdaysCommand { People = people };
        var handler = new BirthdayHandler(_serviceMock.Object);
        _serviceMock.Setup(s => s.IsBirthdayToday(It.IsAny<DateTime>())).Returns(isBirthday);

        //Act
        var birthdayCelebrants = await handler.Handle(command, new CancellationToken());

        //Assert      
        birthdayCelebrants.Should().NotBeNull();
        birthdayCelebrants.Should().HaveCount(expectedBirthdayCelebrants);
    }


}
