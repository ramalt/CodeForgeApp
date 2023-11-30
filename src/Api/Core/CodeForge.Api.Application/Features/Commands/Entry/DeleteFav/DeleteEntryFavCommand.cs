using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.CreateFav;

public class DeleteEntryFavCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }

    public DeleteEntryFavCommand(Guid createdBy, Guid id)
    {
        CreatedBy = createdBy;
        Id = id;
    }

    public DeleteEntryFavCommand()
    {
        
    }
}
