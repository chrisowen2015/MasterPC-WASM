/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class AddCPUFunction
    {
        private readonly ICPUService _cpuService;

        public AddCPUFunction(ICPUService cpuService)
        {
            _cpuService = cpuService;
        }

        [Function("AddCPUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cpu")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var cpuVM = JsonSerializer.Deserialize<CPUVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            string cpuId = await _cpuService.AddCPUAsync(cpuVM);

            return new OkObjectResult(cpuId);
        }
    }
}
*/