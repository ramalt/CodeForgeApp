using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }

    public CreateEntryFavCommand(Guid createdBy, Guid id)
    {
        CreatedBy = createdBy;
        Id = id;
    }

    public CreateEntryFavCommand()
    {
        
    }
}
