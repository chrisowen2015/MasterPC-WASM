using MasterPC_WASM.Services;
using MasterPC_WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;

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

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ILocalStorageServiceClient, LocalStorageServiceClient>();
builder.Services.AddScoped<ICPUService, CPUServiceClient>();
builder.Services.AddScoped<ICaseService, CaseServiceClient>();
builder.Services.AddScoped<ICPUCoolerService, CPUCoolerServiceClient>();
builder.Services.AddScoped<IGPUService, GPUServiceClient>();
builder.Services.AddScoped<IMotherboardService, MotherboardServiceClient>();
builder.Services.AddScoped<IPSUService, PSUServiceClient>();
builder.Services.AddScoped<IRAMService, RAMServiceClient>();
builder.Services.AddScoped<IStorageService, StorageServiceClient>();

await builder.Build().RunAsync();
