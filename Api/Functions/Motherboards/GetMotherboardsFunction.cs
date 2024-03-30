using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Api.Functions.Motherboards
{
    public class GetMotherboardsFunction(IMotherboardService motherboardService)
    {
        private readonly IMotherboardService _motherboardService = motherboardService;

        [Function("GetMotherboardsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "motherboards")] HttpRequest req)
        {
            var motherboards = await _motherboardService.GetMotherboardsAsync();

            return new OkObjectResult(motherboards);
        }
    }
}
