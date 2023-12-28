using CodeForge.Common.Events.Entry;
using CodeForge.Common.Events.EntryComment;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CodeForge.Projections.FavoriteService.Services;

public class FavoriteService
{
    private readonly string _connectionString;


    public FavoriteService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateEntryFav(CreateEntryFavEvent entryFavEvent)
    {
        using var connection = new SqlConnection(_connectionString);

        try
        {
            await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, OwnerId, CreatedDate) VALUES(@Id, @EntryId, @OwnerId, GETDATE())", new
            {
                Id = Guid.NewGuid(),
                EntryId = entryFavEvent.EntryId,
                OwnerId = entryFavEvent.CreatedBy
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return;
        }
    }

    public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE EntryId = @EntryId AND OwnerId = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById = @event.CreatedBy
            });
    }

    public async Task CreateEntryCommentFav(CreateEntryCommentFavEvent @event)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("INSERT INTO EntryCommentFavorite (Id, EntryCommentId, OwnerIdß, CreatedDate) VALUES(@Id, @EntryCommentId, @CreatedById, GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.EntryCommentId,
                CreatedById = @event.CreatedBy
            });
    }

    public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE EntryCommentId = @EntryCommentId AND OwnerId = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.Id,
                CreatedById = @event.CreatedBy
            });
    }
}
