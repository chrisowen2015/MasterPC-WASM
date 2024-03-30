using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Cases
{
    public class AddCPUsFunction(ICPUService cpuService)
    {
        private readonly ICPUService _cpuService = cpuService;

        [Function("AddCPUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cpus/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var cpuVMList = JsonSerializer.Deserialize<List<CPUVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> cpuIds = await _cpuService.AddCPUsAsync(cpuVMList);

                return new CreatedResult(string.Empty, cpuIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
