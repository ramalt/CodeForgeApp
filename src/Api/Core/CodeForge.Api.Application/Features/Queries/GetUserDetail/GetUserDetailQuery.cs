using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.Queries;
using MediatR;

namespace CodeForge.Api.Application.Features.Queries.GetUserDetail;


public class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName;
    }
}


public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper mapper;

    public GetUserDetailQueryHandler(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        this.mapper = mapper;
    }

    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        Domain.Models.User dbUser = null;

        if (request.UserId != Guid.Empty)
            dbUser = await _manager.User.GetByIdAsync(request.UserId);
        else if (!string.IsNullOrEmpty(request.UserName))
            dbUser = await _manager.User.GetSingleAsync(i => i.UserName == request.UserName);

        // TODO if both are empty, throw new exception

        return mapper.Map<UserDetailViewModel>(dbUser);
    }
}