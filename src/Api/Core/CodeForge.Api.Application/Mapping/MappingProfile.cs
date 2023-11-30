using AutoMapper;
using CodeForge.Api.Domain.Models;
using CodeForge.Common.ViewModels.Queries;
using CodeForge.Common.ViewModels.RequestModels;

namespace CodeForge.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>().ReverseMap();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();

        CreateMap<CreateEntryCommand, Entry>().ReverseMap();
    }
}
