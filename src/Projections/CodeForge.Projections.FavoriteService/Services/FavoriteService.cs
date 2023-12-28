using CodeForge.Common.Events.Entry;
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

        await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, OwnerId, CreateDate) VALUES(@Id, @EntryId, @OwnerId, GETDATE())", new
        {
            Id = Guid.NewGuid(),
            EntryId = entryFavEvent.EntryId,
            OwnerId = entryFavEvent.CreatedBy
        });
    }
}
