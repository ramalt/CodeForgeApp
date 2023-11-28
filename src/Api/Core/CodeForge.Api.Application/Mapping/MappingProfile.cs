using AutoMapper;
using CodeForge.Api.Domain.Models;
using CodeForge.Common.ViewModels.Queries;

namespace CodeForge.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>().ReverseMap();
    }
}
