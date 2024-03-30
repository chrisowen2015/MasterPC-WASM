using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;

namespace Api.Functions.PSUs
{
    public class GetPSUFunction(IPSUService psuService)
    {
        private readonly IPSUService _psuService = psuService;

        [Function("GetPSUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "psu/{id}")] HttpRequest req, string id)
        {
            PSUVM psuVM = await _psuService.GetPSUByIdAsync(id);

            return new OkObjectResult(psuVM);
        }
    }
}
