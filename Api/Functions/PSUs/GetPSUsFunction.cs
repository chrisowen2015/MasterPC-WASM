using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Api.Functions.PSUs
{
    public class GetPSUsFunction(IPSUService psuService)
    {
        private readonly IPSUService _psuService = psuService;

        [Function("GetPSUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "psus")] HttpRequest req)
        {
            var psus = await _psuService.GetPSUsAsync();

            return new OkObjectResult(psus);
        }
    }
}
