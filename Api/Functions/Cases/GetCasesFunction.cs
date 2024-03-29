using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;

namespace Api.Functions.Cases
{
    public class GetCasesFunction(ICaseService caseService)
    {
        private readonly ICaseService _caseService = caseService;

        [Function("GetCasesFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cases")] HttpRequest req)
        {
            var cases = await _caseService.GetCasesAsync();

            return new OkObjectResult(cases);
        }
    }
}
