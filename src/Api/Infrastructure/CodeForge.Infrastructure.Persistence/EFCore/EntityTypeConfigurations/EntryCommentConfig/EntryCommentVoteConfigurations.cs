using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryCommentConfig;

public class EntryCommentVoteConfigurations : EntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentVote", CodeForgeAppContext.DEFAULT_SCHEMA);

        builder.HasOne(cv => cv.EntryComment)
            .WithMany(ec => ec.EntryCommentVotes)
            .HasForeignKey(cv => cv.EntryCommentId);

    }
}
