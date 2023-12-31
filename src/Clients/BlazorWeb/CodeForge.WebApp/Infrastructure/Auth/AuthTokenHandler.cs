using Blazored.LocalStorage;
using CodeForge.WebApp.Infrastructure.Extensions;

namespace CodeForge.WebApp.Infrastructure.Services.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ISyncLocalStorageService syncLocalStorageService;

    public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
    {
        this.syncLocalStorageService = syncLocalStorageService;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = syncLocalStorageService.GetToken();

        Console.WriteLine($"Token from ISyncLocalStorageService {token}");

        if (!string.IsNullOrEmpty(token) && (request.Headers.Authorization is null || string.IsNullOrEmpty(request.Headers.Authorization.Parameter)))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}
