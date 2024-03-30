using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class GetCPUCoolerFunction(ICPUCoolerService cpuCoolerService)
    {
        private readonly ICPUCoolerService _cpuCoolerService = cpuCoolerService;

        [Function("GetCPUCoolerFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cpu-cooler/{id}")] HttpRequest req, string id)
        {
            CPUCoolerVM cpuCoolerVM = await _cpuCoolerService.GetCPUCoolerByIdAsync(id);

            return new OkObjectResult(cpuCoolerVM);
        }
    }
}
