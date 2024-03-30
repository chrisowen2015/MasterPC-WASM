using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Cases
{
    public class AddCPUCoolersFunction(ICPUCoolerService cpuCoolerService)
    {
        private readonly ICPUCoolerService _cpuCoolerService = cpuCoolerService;

        [Function("AddCPUCoolersFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cpu-coolers/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var cpuCoolerVMList = JsonSerializer.Deserialize<List<CPUCoolerVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> cpuIds = await _cpuCoolerService.AddCPUCoolersAsync(cpuCoolerVMList);

                return new CreatedResult(string.Empty, cpuIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
