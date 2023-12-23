using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CodeForge.WebApp.Components;
using CodeForge.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.ConfigureHttpClient();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.ConfigureAuth();
builder.Services.ConfigureServices();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
