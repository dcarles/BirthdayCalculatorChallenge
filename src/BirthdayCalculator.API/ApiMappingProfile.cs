using AutoMapper;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.ViewModels;

namespace BirthdayCalculator.API;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<PersonDTO, Person>().ReverseMap();      
    }
}
