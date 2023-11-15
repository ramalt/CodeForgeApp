using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryConfig;

public class EntryFavoriteConfigurations : EntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryFavorite", CodeForgeAppContext.DEFAULT_SCHEMA);

        builder.HasOne(ef => ef.Entry)
            .WithMany(e => e.EntryFavorites)
            .HasForeignKey(ef => ef.EntryId);

        builder.HasOne(ef => ef.Owner)
            .WithMany(o => o.EntryFavorites)
            .HasForeignKey(ef => ef.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
