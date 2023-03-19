using AutoMapper;
using BirthdayCalculator.Domain.Commands;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.ViewModels;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayCalculator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BirthdayController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<BirthdayController> _logger;
    private readonly IMediator _mediator;

    public BirthdayController(ILogger<BirthdayController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(Name = "CalculateBirthdayCelebrants")]
    public async Task<IActionResult> Post([FromBody, CustomizeValidator(RuleSet = "Post")] BirthdayRequest request)
    {
        var birthdayCommand = new CalculateBirthdaysCommand { People = request.People?.Select(p => _mapper.Map<Person>(p)) };
        var birthdayCelebrants = await _mediator.Send(birthdayCommand);
        var response = new BirthdayResponse { BirthdayCelebrants = birthdayCelebrants.Select(p => _mapper.Map<PersonDTO>(p)) };
        return Ok(response);
    }
}
