using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    public UserRepository(CodeForgeAppContext context) : base(context)
    {
    }
}
