using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.RetrieveEtfs;

[ApiController]
[Route("api")]
public sealed class RetrieveEtfsEndpoint : ControllerBase
{
    [HttpGet]
    [Route("etfs")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              CancellationToken cancellationToken = default)
    {
        var query = new RetrieveEtfsQuery();

        var response = await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return new OkObjectResult(response);
    }
}