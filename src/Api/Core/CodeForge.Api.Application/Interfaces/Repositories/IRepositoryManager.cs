namespace CodeForge.Api.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    IEntryRepository Entry { get; }
    IUserRepository User { get; }
    IEntryCommentRepository EntryComment { get; }

    Task<int> SaveAsync();
    void Save();
}
