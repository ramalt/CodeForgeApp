using System.Reflection;
using CodeForge.Api.Domain.Models;
using CodeForge.Api.Domain.Models.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Infrastructure.Persistence.EFCore.Contexts;

public class CodeForgeAppContext : DbContext
{
    public CodeForgeAppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    const string DEFAULT_SCHEMA = "dbo";

    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }
    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public DbSet<User> Users { get; set; }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => (Entity)e.Entity);
        PrepareAddedEntites(addedEntities.ToList());
    }
    private void PrepareAddedEntites(List<Entity> entities) => entities.ForEach(e => e.CreatedDate = DateTime.Now);

}
