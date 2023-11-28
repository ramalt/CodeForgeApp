namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    IEntryRepository Entry { get; }
    IUserRepository User { get; }

    Task SaveAsync();
    void Save();
}
