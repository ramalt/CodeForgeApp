using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations;

public class EmailConfirmationConfigurations : EntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("EmailConfirmation", CodeForgeAppContext.DEFAULT_SCHEMA);
    }
}
