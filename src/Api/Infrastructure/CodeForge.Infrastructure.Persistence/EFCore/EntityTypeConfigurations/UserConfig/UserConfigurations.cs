using CodeForge.Api.Domain.Models;
using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeForge.Infrastructure.Persistence.EFCore.EntityTypeConfigurations.UserConfig;

public class UserConfigurations : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("User", CodeForgeAppContext.DEFAULT_SCHEMA);
    }
}
