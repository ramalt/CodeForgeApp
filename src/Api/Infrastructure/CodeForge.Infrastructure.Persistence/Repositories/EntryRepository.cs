using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class EntryRepository : BaseRepository<Entry>,  IEntryRepository
{

    public EntryRepository(CodeForgeAppContext context) : base(context)
    {
    }

}
