using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Infrastructure.Persistence.EFCore;
using CodeForge.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeForge.Infrastructure.Persistence;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterSqlServer(config);
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
