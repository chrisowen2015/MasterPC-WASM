using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.GPUs
{
    public class AddGPUsFunction(IGPUService gpuService)
    {
        private readonly IGPUService _gpuService = gpuService;

        [Function("AddGPUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "gpus/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var gpus = JsonSerializer.Deserialize<List<GPUVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> cpuIds = await _gpuService.AddGPUsAsync(gpus);

                return new CreatedResult(string.Empty, cpuIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
