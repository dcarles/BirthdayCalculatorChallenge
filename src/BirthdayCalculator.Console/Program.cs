# nullable disable
using AutoMapper;
using BirthdayCalculator.Console;
using BirthdayCalculator.Domain;
using BirthdayCalculator.Domain.Commands;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.Domain.Services;
using BirthdayCalculator.ViewModels;
using BirthdayCalculator.ViewModels.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


//setup our DI
var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddScoped<IBirthdayService, BirthdayService>()
    .AddScoped<IDateTimeProvider, DateTimeProvider>()
    .AddScoped<IOutputFormatter, BirthdayResponseFormatter>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BirthdayHandler>())
    .AddAutoMapper(typeof(ConsoleMappingProfile))
    .AddFluentValidationAutoValidation(config =>
    {
        config.DisableDataAnnotationsValidation = true;
    }).AddValidatorsFromAssemblyContaining<PersonValidator>()
    .BuildServiceProvider();

//Get required Services
var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
var birthdayService = serviceProvider.GetService<IBirthdayService>();
var mediator = serviceProvider.GetService<IMediator>();
var mapper = serviceProvider.GetService<IMapper>();
var outputFormatter = serviceProvider.GetService<IOutputFormatter>();

logger.LogDebug("Starting application");

//Validate argument has been passed
if (args.Length == 0)
{
    Console.WriteLine("Please enter a path for the file with the list of people and their birthdays");
    return 1;
}

//Check that file exists
string jsonFilePath = args[0];
if (!File.Exists(jsonFilePath))
{
    Console.WriteLine("The specified file does not exist.");
    return 1;
}

//Load file and parse as birthdayRequest
using var reader = new StreamReader(jsonFilePath);
var request = new BirthdayRequest();
try
{
    request = PeopleFileLoader.Load(reader);
}
catch (FileLoadException ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}
catch (Exception ex)
{
    Console.WriteLine("There was a problem loading the file, please contact application administrator");
    logger.LogDebug(ex, "Unhandled exception loading the file");
    return 1;
}

//Validate File Content
var validator = serviceProvider.GetService<IValidator<BirthdayRequest>>();
ValidationResult result = await validator.ValidateAsync(request);
if (!result.IsValid)
{
    Console.WriteLine("Some items in the file are invalid. Please see the following validation errors and correct them before loading the file again:");
    foreach (var item in result.Errors)
    {
        Console.WriteLine($"{item.PropertyName}: {item.ErrorMessage}");      
    }
    return 1;
}

//Check for Today's birthdays
var birthdayCommand = new CalculateBirthdaysCommand { People = request.People?.Select(p => mapper.Map<Person>(p)) };
var birthdayCelebrants = await mediator.Send(birthdayCommand);
var response = new BirthdayResponse { BirthdayCelebrants = birthdayCelebrants.Select(p => mapper.Map<PersonDTO>(p)) };

//Display birthdays
var outputText = outputFormatter.Format(response);
System.Console.WriteLine(outputText);


logger.LogDebug("Application completed");

return 0;