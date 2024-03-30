using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class GetGPUFunction(IGPUService gpuService)
    {
        private readonly IGPUService _gpuService = gpuService;

        [Function("GetGPUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "gpu/{id}")] HttpRequest req, string id)
        {
            GPUVM gpuVM = await _gpuService.GetGPUByIdAsync(id);

            return new OkObjectResult(gpuVM);
        }
    }
}
