using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForge.Common.Events.User;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CodeForge.Projections.UserService.Services;

public class UserService
{
    private string connStr;

    public UserService(IConfiguration configuration)
    {
        connStr = configuration.GetConnectionString("SqlServer");
    }

    public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
    {
        var guid = Guid.NewGuid();


        using var connection = new SqlConnection(connStr);

        await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (Id, CreatedDate, OldEmailAddress, NewEmailAddress) VALUES (@Id, GETDATE(), @OldEmailAddress, @NewEmailAddress)",
                new
                {
                    Id = guid,
                    OldEmailAddress = @event.OldEmail ?? "null",
                    NewEmailAddress = @event.NewEmail
                });

        return guid;
    }
}
