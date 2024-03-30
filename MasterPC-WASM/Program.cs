using MasterPC_WASM.Services;
using MasterPC_WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    options.ProviderOptions.LoginMode = "redirect";
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("superusers", policy =>
    {
        policy.RequireAssertion(context => context.User.HasClaim(c =>
        {
            return c.Type == "oid" && c.Value.Contains("2db79c27-24a9-4c74-b63c-9076b7d20d68");
        }));
    });
});

builder.Services.AddScoped<ICPUService, CPUServiceClient>();
builder.Services.AddScoped<ICaseService, CaseServiceClient>();
builder.Services.AddScoped<ICPUCoolerService, CPUCoolerServiceClient>();

await builder.Build().RunAsync();
