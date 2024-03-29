using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using Shared.View_Models;

namespace Api.Functions.Cases
{
    public class GetCaseFunction(ICaseService caseService)
    {
        private readonly ICaseService _caseService = caseService;

        [Function("GetCaseFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "case/{id}")] HttpRequest req, string id)
        {
            CaseVM caseVM = await _caseService.GetCaseByIdAsync(id);

            return new OkObjectResult(caseVM);
        }
    }
}
