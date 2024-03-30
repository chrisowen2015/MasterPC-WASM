using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Services;
using Api.Data;
using Microsoft.EntityFrameworkCore;
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
        services.AddScoped<ICPUCoolersRepository, CPUCoolersRepository>();
        services.AddScoped<ICPUCoolerService, CPUCoolerServiceAPI>();
        services.AddScoped<IGPUsRepository, GPUsRepository>();
        services.AddScoped<IGPUService, GPUServiceAPI>();
        services.AddScoped<IMotherboardsRepository, MotherboardsRepository>();
        services.AddScoped<IMotherboardService, MotherboardServiceAPI>();
    })
    .Build();

host.Run();
