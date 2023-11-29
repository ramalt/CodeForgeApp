namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    IEntryRepository Entry { get; }
    IUserRepository User { get; }

    Task<int> SaveAsync();
    void Save();
}
