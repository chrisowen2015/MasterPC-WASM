using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;

namespace Api.Functions.RAMs
{
    public class GetRAMFunction(IRamService ramService)
    {
        private readonly IRamService _ramService = ramService;

        [Function("GetRAMFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ram/{id}")] HttpRequest req, string id)
        {
            RAMVM ramVM = await _ramService.GetRamByIdAsync(id);

            return new OkObjectResult(ramVM);
        }
    }
}
