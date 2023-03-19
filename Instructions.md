# Birthday Calculation Tech Challenge

In this document, I give a brief explanation about how I tackled the test, tech architecture, how you can test the system, and what I would have done if i had more time.

The name of the application is BirthdayCalculator. Whenever BirthdayCalculator is referred, it represents the system as a whole.

### Tech Stack

For this test I used following framework/libraries:

* **Development Platform** : .Net 6
* **Web API Projects** : ASP.NET 6
* **Unit Testing Framework/Libraries** : xUnit, Moq
* **Other Libraries** : Automapper (for easy mapping between models), MediatR (for easy apply of mediator pattern), Fluent Validation (for validation rules), Swagger (for api doc and test), System.Text.Json (for parsing file content and requests)

### How to test

You can test via Console (main project as required by task) or API (bonus project and accepting just a specific json format)

#### BirthdayCalculator.Console
1)  Build the project (including package restore) in either release or debug mode.
2) Open a console app and go to where your project is located and into the following path inside your project: "src\BirthdayCalculator.Console\bin\[Release OR Debug]\net6.0" (Notice that you need to set it to Release or Debug depending on what mode you build)
3) Create a json file to load and test in any place you like in your system with the appropiate data (or you can use the example ones provided in the root of the solution) 
4) in the console use the command BirthdayCalculator.Console.exe "YOUR_FILE_PATH" (Where "YOUR_FILE_PATH" is the full absolute path to the file you want to use)
5) You should get the results saying that either there is no birthdays, or showing which people on file has birthday today, or explanation of any validation issue (file not found, invalid file content, etc)

For the file you can set the data in any of the following 2 formats:

- Format 1 (as specified by the task)
 ```
[
    ["Doe", "John", "1982/10/08"],
    ["Wayne", "Bruce", "1965/01/30"],
    ["Gaga", "Lady", "1986/03/28"],
    ["Curry", "Mark", "1988/02/29"]
]
```
- Format 2 (A more clear and proper json format)
 ```
 [
	{
      "firstName": "John",
      "lastName": "Doe",
      "birthDate": "1982-10-08"
    },
	{
      "firstName": "Bruce",
      "lastName": "Wayne",
      "birthDate": "1965-01-30"
    },
	{
      "firstName": "Lady",
      "lastName": "Gaga",
      "birthDate": "1986-03-19"
    },
	{
      "firstName": "Mark",
      "lastName": "Curry",
      "birthDate": "1988-02-29"
    }
]
```
### Architecture Overview
The architecture of the project was done following the [Clean Architecture principles](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures). There are 3 projects (plus a bonus project to show how UI can be changed): Console, Domain, ViewModels, API (bonus project).

#### BirthdayCalculator.Console
This project is the entry point of the system, is a console app that receives the path of a file with a list of person and their birthdates as parameter , and output all people that have birthday today. It contains:
- The main program file for DI and main logic 
- A FileLoaderthat load and parse the file in a BirthdayRequest
- A response formatter that outputs the result in the console

#### BirthdayCalculator.Domain
This project contains the main logic of the system. It contains:
- A handler that manage the command to calculate Birthdays and call the service. 
- A Birthday Service that performs the checks to see if person has a birthday
- A DateTimeProvider which is used as a precaution. To make the BirthdayService testable you need to be able to inject Today's date. This could have been achieved by passing DateTime.Now when empty constructor and have additional constructor that receive a date for testing, but if for some reason this Service is instantiated as Singleton then that would cause problem. To be safe is better to inject a simple DateTimeProvider as I did.

#### BirthdayCalculator.ViewModels
This project contains the viewModels (Request/Response plus DTOs) and the Request validator. For the validation im using FluentValidation package.

#### BirthdayCalculator.API
This project was not required by the Task, but thought would be good to show that is easy to have different entryPoints/UI reusing same viewmodels and Domain logic.  Plus was easier at the beginning to test with this without having to think on how to load a file.

### Application Testing

There is a BirthdayCalculator.Tests.Unit project that contains unit tests for each component. For the unit tests I used xUnit testing framework, and Moq for Mocking purposes. No integration tests are written at this stage.


###  What I would have done with more time
 * More tests, there are sufficient tests but not all cases/code is covered, and also there are no integration tests which would be good to have.
 * Better error handling and logging, at the moment there is not very much of this.
 * Would have used Docker to containerize the apps so is more portable and easier to deploy

