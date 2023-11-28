using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;

namespace CodeForge.Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly CodeForgeAppContext _context;

    private readonly Lazy<IEntryRepository> _entryRepository;

    public RepositoryManager(CodeForgeAppContext context)
    {
        _context = context;
        _entryRepository = new Lazy<IEntryRepository>(() => new EntryRepository(_context));
    }

    public IEntryRepository Entry => _entryRepository.Value;
    public void Save() => _context.SaveChanges();
    public Task SaveAsync() => _context.SaveChangesAsync();
}
