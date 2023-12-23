using Blazored.LocalStorage;
using CodeForge.WebApp.Infrastructure.Services;
using CodeForge.WebApp.Infrastructure.Services.Auth;
using CodeForge.WebApp.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace CodeForge.WebApp;

public static class Extensions
{
    public static void ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("WebApiClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5164/");
        });

       services.AddScoped(sp => 
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            return clientFactory.CreateClient("WebApiClient");
        });

    }

    public static void ConfigureAuth(this IServiceCollection services)
    {
        services.AddScoped<AuthTokenHandler>();
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        services.AddBlazoredLocalStorage();
        services.AddAuthorizationCore();

    }

    public static void ConfigureServices(this IServiceCollection services)
    {


        services.AddTransient<IEntryService, EntryService>();
        services.AddTransient<IVoteService, VoteService>();
        services.AddTransient<IFavService, FavService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IIdentityService, IdentityService>();




    }
}
