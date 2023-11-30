using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : BaseRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(DbContext context) : base(context)
    {
    }
}
