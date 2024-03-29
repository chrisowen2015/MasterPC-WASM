using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Services;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Api.Repository;
using Api.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<MasterPcdbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("Connection_String"));
        });

        services.AddScoped<ICPUsRepository, CPUsRepository>();
        services.AddScoped<ICPUService, CPUServiceAPI>();
        services.AddScoped<ICasesRepository, CasesRepository >();
        services.AddScoped<ICaseService, CaseServiceAPI>();
    })
    .Build();

host.Run();
