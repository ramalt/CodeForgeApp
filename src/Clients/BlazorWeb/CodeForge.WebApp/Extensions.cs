namespace CodeForge.WebApp;

public static class Extensions
{
    public static void ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("WebApiClient", config =>
        {
            config.BaseAddress = new Uri("http://http://localhost:5164");
        });

        services.AddScoped(sp =>
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();

            return clientFactory.CreateClient("WebApiClient");
        });
    }
}
