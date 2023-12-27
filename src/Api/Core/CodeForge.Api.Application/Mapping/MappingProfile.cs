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

        CreateMap<UserDetailViewModel, User>().ReverseMap();
        
        CreateMap<CreateEntryCommand, Entry>().ReverseMap();

        CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();

        CreateMap<Entry, GetEntriesViewModel>().ForMember(e => e.CommentCount,
                                                          evm => evm.MapFrom(e => e.EntryComments.Count));


    }
}
