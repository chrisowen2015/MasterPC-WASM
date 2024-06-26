using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class AddCPUFunction(ICPUService cpuService)
    {
        private readonly ICPUService _cpuService = cpuService;

        [Function("AddCPUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cpu/{userId}")] HttpRequest req, string userId)
        {
            if(userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var cpuVM = JsonSerializer.Deserialize<CPUVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string cpuId = await _cpuService.AddCPUAsync(cpuVM);

                return new CreatedResult(string.Empty, cpuId);
            } else
            {
                return new UnauthorizedResult();
            }
        }
    }
}