using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Api.Functions.RAMs
{
    public class GetRAMsFunction(IRamService ramService)
    {
        private readonly IRamService _ramService = ramService;

        [Function("GetRAMsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rams")] HttpRequest req)
        {
            var rams = await _ramService.GetRamsAsync();

            return new OkObjectResult(rams);
        }
    }
}
