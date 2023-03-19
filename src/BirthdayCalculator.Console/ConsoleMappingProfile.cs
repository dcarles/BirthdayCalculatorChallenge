using AutoMapper;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.ViewModels;

namespace BirthdayCalculator.Console;

public class ConsoleMappingProfile : Profile
{
    public ConsoleMappingProfile()
    {
        CreateMap<PersonDTO, Person>().ReverseMap();
    }
}
