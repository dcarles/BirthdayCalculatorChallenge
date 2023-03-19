using AutoFixture;
using AutoMapper;
using BirthdayCalculator.API.Controllers;
using BirthdayCalculator.Domain.Commands;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BirthdayCalculator.Tests.Unit;

public class BirthdayControllerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<ILogger<BirthdayController>> _loggerMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;

    public BirthdayControllerTests()
    {
        _fixture = new Fixture();
        _loggerMock = new Mock<ILogger<BirthdayController>>();
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task Post_ValidApplicationRequest_ReturnsOKResponseWithApplication()
    {
        // Arrange		
        var birthdayRequest = _fixture.Create<BirthdayRequest>();
        var birthdayCelebrants = _fixture.CreateMany<Person>(3);

        _mapperMock.Setup(mapper => mapper.Map<CalculateBirthdaysCommand>(It.IsAny<BirthdayRequest>())).Returns(new CalculateBirthdaysCommand());

        _mediatorMock.Setup(med => med.Send(It.IsAny<CalculateBirthdaysCommand>(), CancellationToken.None)).ReturnsAsync(birthdayCelebrants);

        var applicationController = new BirthdayController(_loggerMock.Object, _mediatorMock.Object, _mapperMock.Object);

        // Act
        var response = await applicationController.Post(birthdayRequest);

        // Assert
        Assert.IsType<OkObjectResult>(response);
        _mediatorMock.Verify(m => m.Send(It.IsAny<CalculateBirthdaysCommand>(), CancellationToken.None), Times.Once);
    }


}
