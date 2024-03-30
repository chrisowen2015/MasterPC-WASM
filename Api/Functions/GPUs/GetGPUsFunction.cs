using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;

namespace Api.Functions.GPUs
{
    public class GetGPUsFunction(IGPUService gpuService)
    {
        private readonly IGPUService _gpuService = gpuService;

        [Function("GetGPUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "gpus")] HttpRequest req)
        {
            var gpus = await _gpuService.GetGPUsAsync();

            return new OkObjectResult(gpus);
        }
    }
}
