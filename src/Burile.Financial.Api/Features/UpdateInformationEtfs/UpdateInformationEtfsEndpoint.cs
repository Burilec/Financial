using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.UpdateInformationEtfs;

[ApiController]
[Route("api")]
public sealed class UpdateInformationEtfsEndpoint
{
    [HttpGet]
    [Route("update-information-etfs")]
    public async Task<IActionResult> GetAsync([FromServices] IMediator mediator,
                                              CancellationToken cancellationToken = default)
    {
        var query = new UpdateInformationEtfsCommand();

        await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return new OkResult();
    }
}