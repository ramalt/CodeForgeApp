using CodeForge.Infrastructure.Persistence.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeForge.Infrastructure.Persistence.EFCore;

public static class Extensions
{
    public static void RegisterSqlServer(this IServiceCollection services, IConfiguration config)
    {
        var sqlServerSettings = config.GetSection("SqlServer");
        services.AddDbContext<CodeForgeAppContext>(option => {
            option.UseSqlServer(sqlServerSettings["ConnectionString"], opt => {
                opt.EnableRetryOnFailure();
            });
        });
    }
}
