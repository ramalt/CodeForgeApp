using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly CodeForgeAppContext _context;

    private readonly Lazy<IEntryRepository> _entryRepository;
    private readonly Lazy<IUserRepository> _userRepository;

    public RepositoryManager(CodeForgeAppContext context)
    {
        _context = context;
        _entryRepository = new Lazy<IEntryRepository>(() => new EntryRepository(_context));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
    }

    public IEntryRepository Entry => _entryRepository.Value;
    public IUserRepository User => _userRepository.Value;

    public void Save() => _context.SaveChanges();
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}
