using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

[ApiController]
[Route("api")]
public sealed class RetrieveEtfsEndpoint : ControllerBase
{
    [HttpGet]
    [Route("etf")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              CancellationToken cancellationToken = default)
    {
        var query = new RetrieveEtfsQuery();

        var response = await mediator.Send(query, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

        return new OkObjectResult(response);
    }
}