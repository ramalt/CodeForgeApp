using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class EmailConfirmRepository : BaseRepository<EmailConfirmation>, IEmailConfirmRepository
{
    public EmailConfirmRepository(DbContext context) : base(context)
    {
    }
}
