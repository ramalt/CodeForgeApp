using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryConfig;

public class EntryVoteConfigurations : EntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryVote", CodeForgeAppContext.DEFAULT_SCHEMA);

        builder.HasOne(ev => ev.Entry)
            .WithMany(e => e.EntryVotes)
            .HasForeignKey(ev => ev.EntryId);
        
    }
}
