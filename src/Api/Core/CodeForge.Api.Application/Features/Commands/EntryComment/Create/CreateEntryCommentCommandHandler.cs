using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

using Models = CodeForge.Api.Domain.Models;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.Create;

public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public CreateEntryCommentCommandHandler(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
    {
        var dbEntryComment = _mapper.Map<Models.EntryComment>(request);
        await _manager.EntryComment.AddAsync(dbEntryComment);
        await _manager.SaveAsync();

        return dbEntryComment.Id;
    }
}
