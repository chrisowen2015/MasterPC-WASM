using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;

namespace Api.Functions.CPUs
{
    public class GetCPUCoolersFunction(ICPUCoolerService cpuCoolerService)
    {
        private readonly ICPUCoolerService _cpuCoolerService = cpuCoolerService;

        [Function("GetCPUCoolersFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cpu-coolers")] HttpRequest req)
        {
            var cpuCoolers = await _cpuCoolerService.GetCPUCoolersAsync();

            return new OkObjectResult(cpuCoolers);
        }
    }
}
