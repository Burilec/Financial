using Microsoft.AspNetCore.Mvc;

namespace Burile.Financial.Api.Features.UpdateInformationEtfs;

[ApiController]
[Route("api")]
public sealed class UpdateInformationEtfsEndpoint
{
    [HttpPost]
    [Route("update-information-etfs")]
    public async Task<IActionResult> PostAsync([FromServices] IMediator mediator,
                                               CancellationToken cancellationToken = default)
    {
        var query = new UpdateInformationEtfsCommand();

        await mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return new OkResult();
    }
}