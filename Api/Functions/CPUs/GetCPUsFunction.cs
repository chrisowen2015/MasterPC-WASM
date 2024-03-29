using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;

namespace Api.Functions.CPUs
{
    public class GetCasesFunction(ICPUService cpuService)
    {
        private readonly ICPUService _cpuService = cpuService;

        [Function("GetCPUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cpus")] HttpRequest req)
        {
            var cpus = await _cpuService.GetCPUsAsync();

            return new OkObjectResult(cpus);
        }
    }
}
