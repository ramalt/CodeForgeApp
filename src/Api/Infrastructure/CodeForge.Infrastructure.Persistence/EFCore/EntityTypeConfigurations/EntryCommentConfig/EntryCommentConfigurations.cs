using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryCommentConfig;

public class EntryCommentConfigurations : EntityConfiguration<EntryComment>
{
    public override void Configure(EntityTypeBuilder<EntryComment> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryComment", CodeForgeAppContext.DEFAULT_SCHEMA);

        builder.HasOne(ec => ec.Entry)
            .WithMany(e => e.EntryComments)
            .HasForeignKey(ec => ec.EntryId);

        builder.HasOne(ec => ec.Owner)
            .WithMany(o => o.EntryComments)
            .HasForeignKey(ec => ec.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
