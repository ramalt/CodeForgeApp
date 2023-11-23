using Bogus;
using CodeForge.Api.Domain.Models;
using CodeForge.Common.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodeForge.Infrastructure.Persistence.EFCore.Contexts;

public class DataSeeder
{
    public async Task SeedAsync(IConfiguration config)
    {
        var context = CreateDbContext<CodeForgeAppContext>(config);

        //db control
        if(context.Users.Any()) 
            return;
        
        var users = GetUsers();
        var userIds = users.Select(u => u.Id);

        await context.Users.AddRangeAsync(users);

        // entry faker setup
        var guids = Enumerable.Range(0, 150).Select(s => Guid.NewGuid()).ToList();
        int counter = 0;

        var entries = new Faker<Entry>("tr")
            .RuleFor(e => e.Id, e => guids[counter++])
            .RuleFor(e => e.CreatedDate, e => e.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(e => e.Subject, e => e.Lorem.Sentence(5,5))
            .RuleFor(e => e.Content, e => e.Lorem.Paragraph(3))
            .RuleFor(e => e.OwnerId, e => e.PickRandom(userIds))
            .Generate(150);
        
        await context.Entries.AddRangeAsync(entries);

        // Entry Comment faker setup
        var comments = new Faker<EntryComment>("tr") 
            .RuleFor(i => i.Id, e => Guid.NewGuid())
            .RuleFor(i=> i.CreatedDate, i=>i.Date. Between (DateTime.Now.AddDays (-100), DateTime.Now))
            .RuleFor(i => i.Content, i => i.Lorem. Paragraph(2))
            .RuleFor(i=>i.OwnerId, i=>i.PickRandom(userIds)) .RuleFor(i=>i.EntryId, i => i.PickRandom(guids))
            .Generate(1000);
        
        await context.EntryComments.AddRangeAsync(comments);


        await context.SaveChangesAsync();
        
    }

    #region configs
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
            .RuleFor(u => u.Id, u => Guid.NewGuid())
            .RuleFor(u => u.CreatedDate, u => u.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(u => u.FirstName, u => u.Person.FirstName)
            .RuleFor(u => u.LastName, u => u.Person.LastName)
            .RuleFor(u => u.UserName, u => u.Person.UserName)
            .RuleFor(u => u.Email, u => u.Internet.Email())
            .RuleFor(u => u.Password, u => PasswordEncrypter.Encrypt(u.Internet.Password()))
            .RuleFor(u => u.EmailConfirmed, u => u.PickRandom(true, false))
            .Generate(500);
        return result;
        
    }

    // maybe could be dynamic but i like this so much better
    private static T CreateDbContext<T>(IConfiguration config) where T : DbContext
    {
        var contextBuilder = new DbContextOptionsBuilder();
        
        var sqlSettings = config.GetSection("SqlServer");
        contextBuilder.UseSqlServer(sqlSettings["ConnectionString"]);
        contextBuilder.EnableSensitiveDataLogging();

        var context = (T)Activator.CreateInstance(typeof(T), contextBuilder.Options);
        return context;
    }

    # endregion


}
