using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Services;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Api.Repository;

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
    })
    .Build();

host.Run();
