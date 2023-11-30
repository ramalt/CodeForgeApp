using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;

using Models = CodeForge.Api.Domain.Models;

namespace CodeForge.Api.Application.Features.Commands.Entry.Create;

public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;
    public CreateEntryCommandHandler(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var dbEntry = _mapper.Map<Models.Entry>(request);

        await _manager.Entry.AddAsync(dbEntry);

        await _manager.SaveAsync();

        return dbEntry.Id;
    }
}
