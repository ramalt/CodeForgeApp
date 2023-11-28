namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    IEntryRepository Entry { get; }

    Task SaveAsync();
    void Save();
}
