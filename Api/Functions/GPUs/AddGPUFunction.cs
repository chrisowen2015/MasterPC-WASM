using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.CPUs
{
    public class AddGPUFunction(IGPUService gpuService)
    {
        private readonly IGPUService _gpuService = gpuService;

        [Function("AddGPUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "gpu/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var gpuVM = JsonSerializer.Deserialize<GPUVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string gpuId = await _gpuService.AddGPUAsync(gpuVM);

                return new CreatedResult(string.Empty, gpuId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}