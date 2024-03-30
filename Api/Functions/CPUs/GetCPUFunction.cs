using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class GetCPUFunction(ICPUService cpuService)
    {
        private readonly ICPUService _cpuService = cpuService;

        [Function("GetCPUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cpu/{id}")] HttpRequest req, string id)
        {
            CPUVM cpuVM = await _cpuService.GetCPUByIdAsync(id);

            return new OkObjectResult(cpuVM);
        }
    }
}
