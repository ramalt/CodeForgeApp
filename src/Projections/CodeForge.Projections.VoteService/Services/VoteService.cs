using CodeForge.Common.Events.Entry;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CodeForge.Projections.VoteService.Services;

public class VoteService
{
    private readonly string _connectionString;

    public VoteService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateEntryVote(CreateEntryVoteEvent vote)
    {
        await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id, CreateDate, EntryId, VoteType, OwnerId) VALUES (@Id, GETDATE(), @EntryId, @VoteType, @OwnerId)",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = vote.EntryId,
                VoteType = (int)vote.Vote,
                OwnerId = vote.CreatedBy
            });
    }

    public async Task DeleteEntryVote(Guid entryId, Guid userId)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId = @EntryId AND OwnerId = @UserId",
            new
            {
                EntryId = entryId,
                UserId = userId
            });
    }
}