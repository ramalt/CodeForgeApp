using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CodeForge.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace CodeForge.WebApp.Infrastructure.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorage;
    private readonly AuthenticationState anonymous;

    public AuthStateProvider(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
        anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var apiToken = await localStorage.GetToken();

        if (string.IsNullOrEmpty(apiToken))
            return anonymous;

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(apiToken);

        var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "jwtAuthType"));

        return new AuthenticationState(cp);
    }

    public void NotifyUserLogin(string userName, Guid userId)
    {
        var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }, "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(cp));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(anonymous);

        NotifyAuthenticationStateChanged(authState);
    }
}
