using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.CPUCoolers
{
    public class AddCPUCoolerFunction(ICPUCoolerService cpuCoolerService)
    {
        private readonly ICPUCoolerService _cpuCoolerService = cpuCoolerService;

        [Function("AddCPUCoolerFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cpu-cooler/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var cpuCoolerVM = JsonSerializer.Deserialize<CPUCoolerVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string cpuCoolerId = await _cpuCoolerService.AddCPUCoolerAsync(cpuCoolerVM);

                return new CreatedResult(string.Empty, cpuCoolerId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}