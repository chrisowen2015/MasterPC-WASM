using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;

namespace Api.Functions.Motherboards
{
    public class GetMotherboardFunction(IMotherboardService motherboardService)
    {
        private readonly IMotherboardService _motherboardService = motherboardService;

        [Function("GetMotherboardFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "motherboard/{id}")] HttpRequest req, string id)
        {
            MotherboardVM motherboardVM = await _motherboardService.GetMotherboardByIdAsync(id);

            return new OkObjectResult(motherboardVM);
        }
    }
}
