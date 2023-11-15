using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.EntryConfig;

public class EntryConfigurations : EntityConfiguration<Entry>
{
    public override void Configure(EntityTypeBuilder<Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("Entry", CodeForgeAppContext.DEFAULT_SCHEMA);
        
        builder.HasOne(e => e.Owner)
            .WithMany(o => o.Entries)
            .HasForeignKey(e => e.OwnerId);
    }
}
