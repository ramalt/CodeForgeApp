using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryCommentConfig;

public class EntryCommentFavoriteConfigurations : EntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentFavorite", CodeForgeAppContext.DEFAULT_SCHEMA);

        builder.HasOne(cf => cf.EntryComment)
            .WithMany(ec => ec.EntryCommentFavorites)
            .HasForeignKey(cf => cf.EntryCommentId);

        builder.HasOne(cf => cf.Owner)
            .WithMany(o => o.EntryCommentFavorites)
            .HasForeignKey(cf => cf.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
